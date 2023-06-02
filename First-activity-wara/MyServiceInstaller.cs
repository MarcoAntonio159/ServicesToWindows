//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace First_activity_wara
//{
//    internal class MyServiceInstaller
//    {
//    }
//}

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class MyServiceInstaller : Installer
{
    private ServiceProcessInstaller processInstaller;
    private ServiceInstaller serviceInstaller;

    public MyServiceInstaller()
    {
        processInstaller = new ServiceProcessInstaller();
        processInstaller.Account = ServiceAccount.LocalSystem;

        serviceInstaller = new ServiceInstaller();
        serviceInstaller.ServiceName = "MyServiceWara";
        serviceInstaller.DisplayName = "My Service Wara";
        serviceInstaller.StartType = ServiceStartMode.Automatic;

        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }
}
