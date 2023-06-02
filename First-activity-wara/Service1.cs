using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace First_activity_wara
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            double intervaloConsulta = double.Parse(ConfigurationManager.AppSettings["intervaloConsulta"]);

            timer = new System.Timers.Timer();
            //timer.Interval = intervaloConsulta * 60 * 1000;
            timer.Interval = intervaloConsulta * 60000;

            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }

        private async void TimerElapsed (object sender, ElapsedEventArgs e)
        {
            int lastID = GetLastID();

            if(lastID >= 5)
            {
                return;
            }

            int nextID = lastID + 1;
            string url = $"https://jsonplaceholder.typicode.com/todos/{nextID}";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    dynamic data = await response.Content.ReadAsAsync<dynamic>();

                    int userID = data.userId;
                    string title = data.title;
                    bool completed = data.completed;

                    InsertToTable(nextID, userID, title, completed);

                    SaveInLog(nextID, userID, title, completed);
                } catch(Exception ex)
                {
                    RegisterErrorInLog(nextID, ex.Message);
                }
            }
        }

        private int GetLastID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            int lastID = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MAX(ID) FROM RegisterURL";
                SqlCommand command = new SqlCommand(query, connection);

                object result = command.ExecuteScalar();

                if(result != DBNull.Value)
                {
                    lastID = Convert.ToInt32(result);
                }
            }
            return lastID;
        }

        private void InsertToTable(int id, int userID, string title, bool completed)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            using(SqlConnection connection = new SqlConnection( connectionString))
            {
                connection.Open();

                string query = "INSERT INTO RegisterURL (ID, UserID, Title, Completed, ConsultationDate) " +
                    "VALUES (@ID, @UserID, @Title, @Completed, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Completed", completed);

                cmd.ExecuteNonQuery();
            }
        }

        private void SaveInLog(int id, int userID, string title, bool completed)
        {
            string logFolderPath = @"D:\WaraServiceLog";
            string logFilePath = Path.Combine(logFolderPath, "SaveRegister.log");

            // Verificar si la carpeta existe, sino la crea
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // Verificar si el archivo existe, sino la crea
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            // Crear el contenido del registro
            string logEntry = $"Registro guardado con exito: ID: {id}, UserID: {userID}, Title: {title}, Completed: {completed}";

            try
            {
                // Escribir el registro en el archivo de log
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Manejar el error al guardar en el log
                Console.WriteLine($"Error al guardar en el log: {ex.Message}");
            }
        }


        private void RegisterErrorInLog(int id, string errorMessage)
        {
            string logFolderPath = @"D:\WaraSolutionsLog";
            string logFilePath = Path.Combine(logFolderPath, "ErrorRegister.log");

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            string logEntry = $"Error al guardar el ID: {id}, Error: {errorMessage}";

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar el error en el log: {ex.Message}");
            }
        }
    }
}
