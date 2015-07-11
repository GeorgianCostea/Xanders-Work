using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace quiz6
{
    public partial class Form1 : Form
    {
        GMapOverlay overlayOne;
        String contry;

         private TcpListener tcpListener;
        private Thread listenThread;
        private int connectedClients = 0;
        private delegate void WriteMessageDelegate(string msg);

        public Form1()
        {
            InitializeComponent();

            StartServer();
        }

        private void mapexplr_Load(object sender, EventArgs e)
        {
            mapexplr.SetCurrentPositionByKeywords("CANADA");
            mapexplr.Position = new PointLatLng(43.3895508, -80.4043501);
            mapexplr.MapProvider = GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            mapexplr.MinZoom = 3;
            mapexplr.MaxZoom = 17;
            mapexplr.Zoom = 10;
            mapexplr.Manager.Mode = AccessMode.ServerAndCache;

            overlayOne = new GMapOverlay(mapexplr, "OverlayOne");


            mapexplr.SetBounds(0, 0, ClientRectangle.Width, ClientRectangle.Height);

        }

         private void StartServer()
        {

            //tcp listener used to listen on any ip addess but must be on port 5222
            IPAddress ip = IPAddress.Parse("192.168.1.4");
            this.tcpListener = new TcpListener(ip, 2381);

            this.listenThread = new Thread(new ThreadStart(ListenForClients));

            
            this.listenThread.Start();

            label1.Text = IPAddress.Parse(((IPEndPoint)tcpListener.LocalEndpoint).Address.ToString()) +
      	                                       "on port number " + ((IPEndPoint)tcpListener.LocalEndpoint).Port.ToString();
          }


        private void ListenForClients()
        {
            this.tcpListener.Start();
            while (true) // Never ends until the Server is closed.
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();
                //create a thread to handle communication 
                //with connected client
                connectedClients++; // Increment the number of clients that have communicated with us.
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }


        private void HandleClientComm(object client)
        {
            //gets the client thats connected
            TcpClient tcpClient = (TcpClient)client;
            //used to read the message sent by that client
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;
            while (true)
            {

                bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }
                //message has successfully been received

                ASCIIEncoding encoder = new ASCIIEncoding();

                // Convert the Bytes received to a string and display it on the Server Screen
                string msg = encoder.GetString(message, 0, bytesRead);

                //parse the string

                string[] subString = msg.Split('$', ':');

                string theMsg = string.Empty;

                Double latitude = Double.Parse(subString[1]);
                Double longitude = Double.Parse(subString[2]);

                plotPointsOnMap(latitude, longitude);

            }
            tcpClient.Close();
        }

        private void plotPointsOnMap(Double latitude,Double longitude)
        {
            overlayOne.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(latitude, longitude)));
            mapexplr.Overlays.Add(overlayOne);
        }



        private void CloseIt(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
          //  this.listenThread.Abort();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
