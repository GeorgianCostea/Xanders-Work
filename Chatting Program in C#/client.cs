using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public partial class FrmChat : Form
    {

        System.Net.Sockets.TcpClient clientSideSocket = new System.Net.Sockets.TcpClient();
        NetworkStream networkStream = default(NetworkStream);
        string readData = null;
        

        public FrmChat()
        {
           
            InitializeComponent();
        }




        private void btnConnect_Click(object sender, EventArgs e)
        {
            string IpAddress = null;

            if (txtIp.Text == "Default")
            {
                IpAddress = "127.0.0.1";
            }
            else
            {
                System.Net.IPAddress ParseIpAddress = null;

                bool IsValid = System.Net.IPAddress.TryParse(txtIp.Text, out ParseIpAddress);

                if (IsValid == true)
                {
                    if (ParseIpAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IpAddress = ParseIpAddress.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Your ip is invalid , please enter a valid IPv4 ");
                    }
                }
                else
                {
                    MessageBox.Show("Your ip is invalid , please enter a valid IPv4 ");
                }

            }
  
            try
            {
                clientSideSocket = new System.Net.Sockets.TcpClient();
                clientSideSocket.Connect(IpAddress, 1337);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(" Make sure that the server is working properly for more details please check  " + ex);
                return;
            }

            readData = "Conected to Chat Server ...";
            DisplayMessage();
            networkStream = clientSideSocket.GetStream();

            byte[] SendMessage = Encoding.ASCII.GetBytes(txtName.Text + "Cool");
            networkStream.Write(SendMessage, 0, SendMessage.Length);
            networkStream.Flush();

            txtName.Enabled = false;
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            
            Thread messageThread = new Thread(receiveMessage);
            messageThread.Start();

          

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] sendMessage = Encoding.ASCII.GetBytes(txtReply.Text + "Cool");
            if (networkStream.CanWrite)
            {
                networkStream.Write(sendMessage, 0, sendMessage.Length);
            }
            else
            {
                MessageBox.Show("Please connect to the SERVER !! ");
                return;
            }
            networkStream.Flush();
            txtReply.Clear();
        }





        private void receiveMessage()
        {
            while (true)
            {
                int bufferSize = 0;
                byte[] ReceiveMessage = new byte[65536];

                networkStream = clientSideSocket.GetStream();

                bufferSize = clientSideSocket.ReceiveBufferSize;
                try
                {
                    networkStream.Read(ReceiveMessage, 0, bufferSize);
                }
                catch (SocketException es)
                {
                    MessageBox.Show("mssage"+ es);
                }
               
                string returnData = System.Text.Encoding.ASCII.GetString(ReceiveMessage);
                returnData = returnData.Substring(0, returnData.IndexOf("Cool"));
                if ((returnData == "Disconnected")||(returnData =="ChangeName"))
                {
                    if (returnData == "ChangeName")
                    {
                      readData="Disconnected From Chat Server ..." + "Please Change your nick name, The name you chose its already taken";
                      DisplayMessage();
                    }
                    txtName.Enabled = true;
                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                    networkStream.Close();
                    clientSideSocket.Close();
                  
            
                    return;
                }
                else
                {
                    readData = "" + returnData;
                    DisplayMessage();
                }
            }
        }

        private void DisplayMessage()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(DisplayMessage));
            else
                txtChat.Text = txtChat.Text + Environment.NewLine + " --> " + readData;
        }

        private void txtReply_KeyPress(object sender, KeyPressEventArgs e)
        {
            //when the user presses enter sends the message.
            if (e.KeyChar == 13)
            {
                byte[] sendMessage = Encoding.ASCII.GetBytes(txtReply.Text + "Cool");
                networkStream.Write(sendMessage, 0, sendMessage.Length);
                networkStream.Flush();
                txtReply.Clear();
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

            readData = "Disconnected From Chat Server ...";
            DisplayMessage();

            byte[] SendMessage = Encoding.ASCII.GetBytes("Close" + "Cool");
            networkStream.Write(SendMessage, 0, SendMessage.Length);
            networkStream.Flush();

            txtName.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            
        }





    }
}
