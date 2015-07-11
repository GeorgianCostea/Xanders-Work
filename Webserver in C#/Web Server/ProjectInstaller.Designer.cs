namespace Web_Server
{
    partial class ProjectInstaller
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
            this.WebServer = new System.ServiceProcess.ServiceProcessInstaller();
            this.WebServerInstall = new System.ServiceProcess.ServiceInstaller();
            // 
            // WebServer
            // 
            this.WebServer.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.WebServer.Password = null;
            this.WebServer.Username = null;
            // 
            // WebServerInstall
            // 
            this.WebServerInstall.ServiceName = "Web_Server";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WebServer,
            this.WebServerInstall});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WebServer;
        private System.ServiceProcess.ServiceInstaller WebServerInstall;
    }
}