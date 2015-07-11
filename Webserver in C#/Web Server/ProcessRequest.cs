/*
 * File Name    : ProcessRequest.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Contains the ProcessRequest class which parses the 
 * browser request , and send the response.
 * 
 */


using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Globalization;
using System.Web;
using SspDLL;

namespace Web_Server
{
	/// <summary>
	/// ProcessRequest is used to receive the request and process it in order to check what protocol is using
	/// and also to get the url ( the file ) and any other values that comes with the request.
	/// </summary>
    public class ProcessRequest
    {
        
        //TcpClient Used to receive
        private TcpClient user;
        private RequestState infoAboutState;
        private RequestStruct newHTTPRequest;
        private ResponseStruct newHTTPResponse;

        /* going to be used to read */
       
        byte[] newBuffer;
        Server serverParrent;
        
        

		/// <summary>
		/// Default constructor that initializes all data members to 
		/// default values 
		/// </summary>
        public  ProcessRequest()
        {
            this.user = null;
            this.infoAboutState = 0;
            this.newHTTPRequest.sizeOfBody = 0;
            this.newHTTPResponse.sizeOfBody = 0;

        }



		/// <summary>
		/// Constructor for the Server class which sets all data members fields accordingly.
		/// </summary>
		/// <param name="newClient">get the new client</param>
		/// <param name="newParrent">the server parent</param>
        public ProcessRequest(TcpClient newClient, Server newParrent)
        {
            this.user = newClient;
            serverParrent = newParrent;
            this.newHTTPRequest.sizeOfBody = 0;
            this.newHTTPResponse.sizeOfBody = 0;
        }




		/// <summary>
		/// This method parses the request in order to send a response to the browser.
		/// </summary>
        public void newProcess()
        {
            //the size of the new buffer which is 8192 by default
            newBuffer = new byte[user.ReceiveBufferSize];
            string completeMessage = "";
            int numberOfBytesRead = 0;
            string myValue = "";
            string myKey = "";

            
            //write to the logger

            //Returns the networkstream in order to be used to send and receive data
            NetworkStream netStream = user.GetStream();


            try
            {
                //binary index 
                int binaryDataIndex = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    //store the bytes that was read into numberofbytesRead and starts at index 0 
                    numberOfBytesRead = netStream.Read(newBuffer, 0, newBuffer.Length);
                    // store the entire message, and decode the entire buffer starting at index 0 into a string.
                    completeMessage = string.Concat(completeMessage, Encoding.ASCII.GetString(newBuffer, 0, numberOfBytesRead));

                    //read the buffer index 
                    int bufferIndex = 0;
                    do
                    {
                        switch (infoAboutState)
                        {
                            case RequestState.METHOD:
								/*get the method in out case will be get*/
                                if (newBuffer[bufferIndex] != ' ')
                                {
                                    newHTTPRequest.method += (char)newBuffer[bufferIndex++];
                                }
                                else
                                {
                                    bufferIndex++;
                                    infoAboutState = RequestState.URL;
                                }
                                break;
                            case RequestState.URL:

								/*get the file that the use is looking for*/
                                if (newBuffer[bufferIndex] == '?')
                                {
                                    bufferIndex++;
                                    myKey = "";
                                    newHTTPRequest.execute = true;
                                    newHTTPRequest.arguments = new Hashtable();
                                    infoAboutState = RequestState.URLPARM;
                                }
                                else if (newBuffer[bufferIndex] != ' ')
                                {
                                    newHTTPRequest.url += (char)newBuffer[bufferIndex++];
                                }
                                else
                                {
                                    
                                    bufferIndex++;
                                   
                                    newHTTPRequest.url = HttpUtility.UrlDecode(newHTTPRequest.url);
                                    infoAboutState = RequestState.URLPARM;
                                }
                                break;
                            case RequestState.URLPARM:
                                
                                if(newBuffer[bufferIndex] == '=')
                                {
                                    bufferIndex++;
                                    myValue = "";
                                    infoAboutState = RequestState.URLVALUE;
                                }
                                else if (newBuffer[bufferIndex] == ' ')
                                {
                                    bufferIndex++;
                                    newHTTPRequest.url = HttpUtility.UrlDecode(newHTTPRequest.url);
                                    infoAboutState = RequestState.VERSION;
                                }
                                else
                                {
                                    myKey += (char)newBuffer[bufferIndex++];
                                }

                                break;
                            case RequestState.URLVALUE:

                                if (newBuffer[bufferIndex] == '&')
                                {
                                    bufferIndex++;
                                    myKey = HttpUtility.UrlDecode(myKey);
                                    myValue = HttpUtility.UrlDecode(myValue);
                                    newHTTPRequest.arguments[myKey] = newHTTPRequest.arguments[myKey] != null ? newHTTPRequest.arguments[myKey] + ", " + myValue : myValue;
                                    myKey = "";
                                    infoAboutState = RequestState.URLPARM;
                                }
                                else if (newBuffer[bufferIndex] == ' ')
                                {
                                    bufferIndex++;
                                    myKey = HttpUtility.UrlDecode(myKey);
                                    myValue = HttpUtility.UrlDecode(myValue);
                                    newHTTPRequest.arguments[myKey] = newHTTPRequest.arguments[myKey] != null ? newHTTPRequest.arguments[myKey] + ", " + myValue : myValue;

                                    newHTTPRequest.url = HttpUtility.UrlDecode(newHTTPRequest.url);
                                    infoAboutState = RequestState.VERSION;
                                }
                                else
                                {
                                    myValue += (char)newBuffer[bufferIndex++];
                                }
                                break;
                            case RequestState.VERSION:


                                if (newBuffer[bufferIndex] == '\r')
                                {
                                    bufferIndex++;
                                }
                                else if (newBuffer[bufferIndex] != '\n')
                                {
                                    newHTTPRequest.version += (char)newBuffer[bufferIndex++];
                                }
                                else
                                {
                                    bufferIndex++;
                                    myKey = "";
                                    newHTTPRequest.headers = new Hashtable();
                                    infoAboutState = RequestState.HEADERKEY;
                                }
                                break;
                            case RequestState.HEADERKEY:

                                if (newBuffer[bufferIndex] == '\r')
                                {
                                    bufferIndex++;
                                }
                                else if (newBuffer[bufferIndex] == '\n')
                                {
                                    bufferIndex++;
                                    if (newHTTPRequest.headers["Content-Length"] != null)
                                    {
                                        newHTTPRequest.sizeOfBody = Convert.ToInt32(newHTTPRequest.headers["Content-Length"]);
                                        this.newHTTPRequest.DataInBody = new byte[this.newHTTPRequest.sizeOfBody];
                                        infoAboutState = RequestState.BODY;
                                    }
                                    else
                                    {
                                        infoAboutState = RequestState.OK;
                                    }

                                }
                                else if (newBuffer[bufferIndex] == ':')
                                {

                                    bufferIndex++;
                                }
                                else if (newBuffer[bufferIndex] != ' ')
                                {
                                    myKey += (char)newBuffer[bufferIndex++];
                                }
                                else
                                {
                                    bufferIndex++;
                                    myValue = "";
                                    infoAboutState = RequestState.HEADERVALUE;
                                }

                                break;
                            case RequestState.HEADERVALUE:

                                if (newBuffer[bufferIndex] == '\r')
                                {
                                    bufferIndex++;
                                }
                                else if (newBuffer[bufferIndex] != '\n')
                                {
                                    myValue += (char)newBuffer[bufferIndex++];
                                }
                                else
                                {
                                    bufferIndex++;
                                    newHTTPRequest.headers.Add(myKey, myValue);
                                    myKey = "";
                                    infoAboutState = RequestState.HEADERKEY;
                                }
                                break;
                            case RequestState.BODY:

                                // Append to request BodyData
                                Array.Copy(newBuffer, bufferIndex, this.newHTTPRequest.DataInBody, binaryDataIndex, numberOfBytesRead - bufferIndex);
                                binaryDataIndex += numberOfBytesRead - bufferIndex;
                                bufferIndex = numberOfBytesRead;
                                if (this.newHTTPRequest.sizeOfBody <= binaryDataIndex)
                                {
                                    infoAboutState = RequestState.OK;
                                }
                                break;

                        }

                    } while (bufferIndex < numberOfBytesRead);

                } while (netStream.DataAvailable);



                newHTTPResponse.version = "HTTP/1.1";


                if (infoAboutState != RequestState.OK)
                {
                    newHTTPResponse.status = (int)ResponseState.BAD_REQUEST;
                }
                else
                {
                    newHTTPResponse.status = (int)ResponseState.OK;
                    
                    //call a function that parses the URL to check of the Extension
                    // create the new headers that are going to be send to the user.
                    this.newHTTPResponse.headers = new Hashtable();
                    this.newHTTPResponse.headers.Add("Server",serverParrent.name);
                    this.newHTTPResponse.headers.Add("Date",DateTime.Now.ToString("r"));
                   
                    // we pass the info about the client and what ever he is looking for 
                    this.serverParrent.OnResponse(ref this.newHTTPRequest, ref this.newHTTPResponse);

                    string HeaderStr = this.newHTTPResponse.version + " " + this.serverParrent.responseStatus[this.newHTTPResponse.status] + "\n";

                    foreach (DictionaryEntry Header in this.newHTTPResponse.headers)
                    {
                        HeaderStr += Header.Key + ": " + Header.Value + "\n";
                    }
           
                    HeaderStr += "\n";
                    byte[] byteHeaderStr = Encoding.ASCII.GetBytes(HeaderStr);

                    //sending the headers
                    netStream.Write(byteHeaderStr, 0, byteHeaderStr.Length);
                    
                       //send body if the file was not found

                    if(this.newHTTPResponse.DataInBody != null)
                    {
                        netStream.Write(this.newHTTPResponse.DataInBody, 0, this.newHTTPResponse.DataInBody.Length);
                    }


                    //check if the new file stream is not emphy so we can open it and read it.
                    if (this.newHTTPResponse.newFileStream != null)
                    {
                        using (this.newHTTPResponse.newFileStream)
                        {
                            byte[] newByte = new byte[user.SendBufferSize];

                            int bytesRead;

							// check for .ssp extension
                            bool isSsp = Verify.verifyFileExtension(newHTTPRequest);


                            if (isSsp == true)
                            {
								/*if .ssp is present than send the file to the processor*/
                                string serverScript = convertToString(newHTTPResponse, newByte);


                                string response = SspProcessor.extractScript(serverScript);

								
                                if (response != null)
                                {
                                 
									byte[] ds = Encoding.ASCII.GetBytes(response);
									netStream.Write(ds, 0, ds.Length);
											
                                }
                                else
                                {
									/*script invalid send error*/
									serverParrent.serverError(ref newHTTPResponse);
									netStream.Write(this.newHTTPResponse.DataInBody, 0, this.newHTTPResponse.DataInBody.Length);
                                }

                            }
                            else
                            {
                                
								/*send the page is its not .ssp*/
                                while ((bytesRead = this.newHTTPResponse.newFileStream.Read(newByte, 0, newByte.Length)) > 0)
                                {
                                    netStream.Write(newByte, 0, bytesRead);
                                }
                                this.newHTTPResponse.newFileStream.Close();
                            }
                            
                        }
                    }
                }
                 

            }
            catch (Exception e)
            {
                //write to log
               Logger.Log(" Exception Occured in processRequest" + e);
            }
            finally
            {
                //close everything

                netStream.Close();
                user.Close();
                if (this.newHTTPResponse.newFileStream != null)
                {
                    this.newHTTPResponse.newFileStream.Close();
                    
                }
                Thread.CurrentThread.Abort();
            }

        }



		/// <summary>
		/// this method converts the byte[] to ascii values.
		/// </summary>
		/// <param name="newHTTPResponse">the response struct to read the file</param>
		/// <param name="buffer">the buffer to read.</param>
		/// <returns></returns>
        public string convertToString(ResponseStruct newHTTPResponse, byte[] buffer)
        {
            int numberOfBytes = 0;
            string file = string.Empty;
            while ((numberOfBytes = this.newHTTPResponse.newFileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                file += Encoding.UTF8.GetString(buffer);
            }
            return file;
        }

    }
}
