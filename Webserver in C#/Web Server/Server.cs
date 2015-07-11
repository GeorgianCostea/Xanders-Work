/*
 * File Name    : Server.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Contains the Server class which si the parrent of HTTPResponse.
 * The class is used to listen to any incomming requests.
 */


using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Net;

namespace Web_Server
{

	/// <summary>
	/// This is an abstract base class for receiving and start/stop the web server.
	/// </summary>
	/// <remarks>
	/// This class will listen to any ip address for page requests. if the stop button is pressed than
	/// the methods in the class will check if thee thread is this alive or not , and terminate.
	/// </remarks>
    public abstract class  Server
    {


		/*-------DataMembers--------*/
        private int portNumber;
        private TcpListener listenForNewUsers;
        public Hashtable responseStatus;
        public string name;
        bool isActive = false;
        Thread startingThread;


		/*-----Getters------*/

        public bool checkThread
        {
            get
            {
                return this.startingThread.IsAlive;
            }
        }


		/// <summary>
		/// Default constructor that initializes all data members to 
		/// default values
		/// </summary>
        public Server()
        {
            this.listenForNewUsers = null;
            this.name = "WebServer";
            this.portNumber = 1337;
            responseStatusInit();
        }

		/// <summary>
		/// Constructor for the Server class which sets all data members fields accordingly.
		/// </summary>
		/// <param name="thePort">the port number on which the program will listen for incomming requests</param>
        public Server(int thePort)
        {
            this.portNumber = thePort;
            this.listenForNewUsers = null;
            this.name = "WebServer";
            responseStatusInit();
        }



		/// <summary>
		/// The purpose of this method is to create responses status.
		/// </summary>
        private void responseStatusInit()
        {
            responseStatus = new Hashtable();

            responseStatus.Add(200, "200 Ok");
            responseStatus.Add(201, "201 Created");
            responseStatus.Add(202, "202 Accepted");
            responseStatus.Add(204, "204 No Content");

            responseStatus.Add(301, "301 Moved Permanently");
            responseStatus.Add(302, "302 Redirection");
            responseStatus.Add(304, "304 Not Modified");

            responseStatus.Add(400, "400 Bad Request");
            responseStatus.Add(401, "401 Unauthorized");
            responseStatus.Add(403, "403 Forbidden");
            responseStatus.Add(404, "404 Not Found");

            responseStatus.Add(500, "500 Internal Server Error");
            responseStatus.Add(501, "501 Not Implemented");
            responseStatus.Add(502, "502 Bad Gateway");
            responseStatus.Add(503, "503 Service Unavailable");
        }




		/// <summary>
		/// The purpose of this method is to listen  for any new requests. It listens to any ip(with a set portnumber)
		/// if a new request is received a new thread is fired in order to run in parallel.
		/// </summary>
        public void ListenForIncoming()
        {
            try
            {
                listenForNewUsers = new TcpListener(IPAddress.Any, portNumber);

                listenForNewUsers.Start();
				/*the loop will run until the stop button is pressed.*/
                do
                {
					/*create new thread*/
                    ProcessRequest newRequest = new ProcessRequest(listenForNewUsers.AcceptTcpClient(), this);
                    Thread newThread = new Thread(new ThreadStart(newRequest.newProcess));
                    newThread.Start();

                } while (isActive);

            }
            catch
            {
                isActive = false;
                listenForNewUsers.Stop();
                if (checkThread)
                {
                    this.startingThread.Abort(); 
                }
            }
        }


        /// <summary>
        /// The purpose of this method is to start the program by creating a new
		/// thread.
        /// </summary>
        public void StartProgram()
        {
            try
            {
                isActive = true;
                this.startingThread = new Thread(new ThreadStart(this.ListenForIncoming));
                this.startingThread.Start();
            }
            catch (Exception e)
            {
                Logger.Log("Exception occured in Server.cs (start program) " + e);
            }
        }




		/// <summary>
		///The purpose of this method is to stop the program by aborting the  program thread.
		///
		/// </summary>
        public void StopProgram()
        {
            try
            {
                bool isAlive = checkThread;
                if (isAlive)
                {
                    isActive = false;
                    listenForNewUsers.Stop();
                    this.startingThread.Abort();
                    
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception occured in Server.cs (start program) " + e);
            }
        }

		/// <summary>
		/// method to be overriten by the child class
		/// </summary>
		/// <param name="RequestReference"></param>
		/// <param name="ResponseReference"></param>
        public abstract void OnResponse(ref RequestStruct RequestReference, ref ResponseStruct ResponseReference);
		/// <summary>
		/// method to be overritten by the child class.
		/// </summary>
		/// <param name="ResponseReference"></param>
		public abstract void serverError(ref ResponseStruct ResponseReference);
    }
}
