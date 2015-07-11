using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;



namespace Server
{
    class Server
    {
         public static  Dictionary<object,object> clientsList = new Dictionary<object,object>();
         public static  TcpClient newClientSocket = default(TcpClient);


        public void ListenForClients()
        {

            TcpListener newServerSocket = new TcpListener(1337);

           

            newServerSocket.Start();

            Console.WriteLine("Server Started You Can Chat :)");
          

            while (true)
            {

                byte[] bytesFrom = new byte[65536];
                string dataFromClient = null;
                int flag = 0;


                newClientSocket = default(TcpClient);
                newClientSocket = newServerSocket.AcceptTcpClient();

                NetworkStream networkStream = newClientSocket.GetStream();

                networkStream.Read(bytesFrom, 0, (int)newClientSocket.ReceiveBufferSize);

                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("Cool"));

                if (clientsList.Count == 0)
                {
                    
                    clientsList.Add(dataFromClient, newClientSocket);

                    broadcast(dataFromClient + " Joined ", dataFromClient, false);

                    Console.WriteLine(dataFromClient + " Joined chat room ");

                    HandleClient client = new HandleClient();

                    client.startClient(newClientSocket, dataFromClient);
                }
                else
                {


                    foreach (KeyValuePair<object, object> Item in clientsList)
                    {

                        if ((String)Item.Key == dataFromClient)
                        {
                            Byte[] lastMessage = null;
                            lastMessage = Encoding.ASCII.GetBytes("ChangeName" + "Cool");
                            networkStream.Write(lastMessage, 0, lastMessage.Length);
                            networkStream.Flush();
                            newClientSocket.Close();
                            flag = 1;
                            break;

                        }
                        else
                        {
                            flag = 0;
                        }
                    }

                    if(flag == 0)
                    {
                        clientsList.Add(dataFromClient, newClientSocket);
                        broadcast(dataFromClient + " Joined ", dataFromClient, false);

                        Console.WriteLine(dataFromClient + " Joined chat room ");

                        HandleClient client = new HandleClient();

                        client.startClient(newClientSocket, dataFromClient);
               
                    }

               
                }
               
            }



            newClientSocket.Close();

            newServerSocket.Stop();

            Console.WriteLine("exit");

            Console.ReadLine();

        }



        public static void broadcast(string message, string UserName, bool flag)
        {
           
            if(clientsList.Count != 0)
            {
                foreach (KeyValuePair<object, object> Item in clientsList)
                {

                    TcpClient broadcastSocket;


                    broadcastSocket = (TcpClient)Item.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    if ((message == "Close")&&(UserName == (String)Item.Key) )
                    {
                        clientsList.Remove(UserName);
                        Byte[] lastMessage = null;

                        lastMessage = Encoding.ASCII.GetBytes("Disconnected" + "Cool");
                        broadcastStream.Write(lastMessage, 0, lastMessage.Length);
                        broadcastStream.Flush();

                        broadcastSocket.Close();
                      

                        return;
                    }
                    else
                    {



                        Byte[] broadcastBytes = null;



                        if (flag == true)
                        {

                            broadcastBytes = Encoding.ASCII.GetBytes(UserName + " replied : " + message + "Cool");

                        }

                        else
                        {

                            broadcastBytes = Encoding.ASCII.GetBytes(message + "Cool");

                        }




                        broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);

                        broadcastStream.Flush();
                    }
                }

            }

        }  

    }
}
