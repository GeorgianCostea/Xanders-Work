/*
 * File Name    : HTTPResponse.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Containes the HTTPResponse class that searches for file in the 
 * folder specified in the configuration file , if the file does not exist than it 
 * will create an error page.
 */



using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;


namespace Web_Server
{

	/// <summary>
	/// HTTPResponse class inherits from the base Server class, and 
	/// is used to search for files in the directory or create a new file error file.
	/// </summary>
    class HTTPResponse : Server
    {
        public string Container;
        public string FileName;


        

		/// <summary>
		/// Default constructor that initializes all data members to 
		/// default value
		/// </summary>
        public HTTPResponse()
            : base()
        {
            this.Container = "c:\\temp";
            this.FileName = "";


        }




		/// <summary>
		/// Constructor for the Server class which sets all data members fields accordingly.
		/// </summary>
		/// <param name="port">the port that the program will listen to</param>
		/// <param name="newContainer">the folder where all the files are</param>
		/// <param name="file">default file</param>
        public HTTPResponse(int port, string newContainer,string file)
            : base(port)
        {
            this.Container = newContainer;
            this.FileName = file;
        }

     

        /// <summary>
        /// This method searches for the file requested by the browser. if it does not exist than it will
		/// send an error page(file not found).
        /// </summary>
        /// <param name="RequestReference"> reference struct with all information about the new request</param>
        /// <param name="ResponseReference">reference struct with information to be send.</param>
        public override void OnResponse(ref RequestStruct RequestReference, ref ResponseStruct ResponseReference)
        {
            string path = this.Container  + RequestReference.url.Replace("/", "\\");
            if (Directory.Exists(path))
            {
                if(File.Exists(path + FileName))
                {
                    path += "\\"+ FileName;
                }
            }


            if (File.Exists(path))
            {
               
                ResponseReference.newFileStream = File.Open(path, FileMode.Open);
              
                RequestReference.headers["Content-type"] = "text";
              
            }
            else
            {

                ResponseReference.status = (int)ResponseState.NOT_FOUND;

                string bodyStr = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\n";
                bodyStr += "<HTML><HEAD>\n";
                bodyStr += "<title>IIS Detailed Error - 404 - Not Found</title> \n";
                bodyStr += "<META http-equiv=Content-Type content=\"text/html; charset=windows-1252\">\n";
                bodyStr += "</HEAD>\n";
                bodyStr += "<BODY>\n";
                bodyStr += "<fieldset><legend>Error Summary</legend>\n";
                bodyStr += "<h2>HTTP Error 404 - Not Found</h2>\n";
                bodyStr += "<h3>The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.</h3>\n";
                bodyStr += "</fieldset>\n ";
                bodyStr += "</BODY></HTML>\n";

                ResponseReference.DataInBody = Encoding.ASCII.GetBytes(bodyStr);

            }
            
        }



		/// <summary>
		/// This function will send an 500 error if there is a problem with the script.
		/// </summary>
		/// <param name="ResponseReference">reference struct with information to be send.</param>
		public override void serverError(ref ResponseStruct ResponseReference)
        {
            ResponseReference.status = (int)ResponseState.SERVER_ERROR;
			string bodyStr = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\n";
			bodyStr += "<HTML><HEAD>\n";
			bodyStr += "<title>Server Error 500 Invalid Script.</title> \n";
			bodyStr += "<META http-equiv=Content-Type content=\"text/html; charset=windows-1252\">\n";
			bodyStr += "</HEAD>\n";
			bodyStr += "<BODY>\n";
			bodyStr += "<fieldset><legend>Error Summary</legend>\n";
			bodyStr += "<h2>Server Error 500 Invalid Script</h2>\n";
			bodyStr += "<h3>The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.</h3>\n";
			bodyStr += "</fieldset>\n ";
			bodyStr += "</BODY></HTML>\n";

			ResponseReference.DataInBody = Encoding.ASCII.GetBytes(bodyStr);
           
        }


    }
}
