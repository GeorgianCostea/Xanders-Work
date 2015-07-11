using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace Web_Server
{
    public partial class WebServer : ServiceBase
    {
        public WebServer()
        {
            InitializeComponent();
            
        }


        Server server;

        protected override void OnStart(string[] args)
        {
			Configuration getConfiguration = new Configuration();
			if (getConfiguration.LoadAllSettings() == true)
			{
				Logger.Log("Web_Server is running.");
				server = new HTTPResponse(getConfiguration.port, getConfiguration.folder, getConfiguration.fileName);
				server.StartProgram();

			}
           

        }

        protected override void OnStop()
        {
			Logger.Log("Web_Server was stopped.");
			server.StopProgram();

        }
    }
}
