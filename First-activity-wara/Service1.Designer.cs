﻿namespace First_activity_wara
{
    partial class Service1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.stTimer)).BeginInit();
            // 
            // stTimer
            // 
            this.stTimer.Enabled = true;
            this.stTimer.Interval = 60000D;
            this.stTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerElapsed);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.stTimer)).EndInit();

        }

        #endregion

        private System.Timers.Timer stTimer;
    }
}
