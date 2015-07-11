using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    class HandleClient
    {
        

        


        string ReceivedDataFromClient;
        TcpClient clientSocket;



        public void startClient(TcpClient inClientSocket, string newDataFromClient)
        {

            this.clientSocket = inClientSocket;

            this.ReceivedDataFromClient = newDataFromClient;


            Thread ctThread = new Thread(CreateChat);

            ctThread.Start();

        }



        private void CreateChat()
        {

            int requestCount = 0;
            byte[] bytesFrom = new byte[65536];
            string dataFromClient = null;
            string count = null;



            requestCount = 0;

            while (true)
            {

                try
                {

                    requestCount = requestCount + 1;

                    NetworkStream networkStream = clientSocket.GetStream();

                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);


                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("Cool"));

                    
                   

                    count = Convert.ToString(requestCount);

                    if (dataFromClient == "Close")
                    {
                        Console.WriteLine(ReceivedDataFromClient + " Disconnected for server");
                        Server.broadcast(dataFromClient, ReceivedDataFromClient, true);
                       // networkStream.Close();

                        return;
                    }
                    else
                    {
                        Console.WriteLine("From client - " + ReceivedDataFromClient + " : " + dataFromClient);
                    }

                    Server.broadcast(dataFromClient, ReceivedDataFromClient, true);

                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                    break;

                }

            }

        }
    }
}
