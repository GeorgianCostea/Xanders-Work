/*
 * File Name    : Configuration.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  :Contains the Configuration class that reads
 * configuration details( port / folder and default file).
 */

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;

namespace Web_Server
{
	/// <summary>
	/// Configuration file reads the configuration file ( located in the same directory as the executatble)
	/// and sets data members to the values read from the file.
	/// </summary>
    public class Configuration
    {
        public string configureSettings { set; get; }
        public string folder { set; get; }
        public int port { set; get; }
        public string fileName { set; get; }


        /// <summary>
        /// Default Constructor that initialize all data members to default values.
        /// </summary>
        public Configuration()
        {
            configureSettings = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ConfigurationFile.exe.config");
            this.folder = "C:\\temp";
            this.port = 1188;
            this.fileName = "";
        }


		/// <summary>
		/// the method will check if the file exists and if does
		/// than i  will open it to read from it .
		/// </summary>
		/// <returns></returns>
        public bool LoadAllSettings()
        {

            //check if the file exists 
            // then open the file
            // create the XML reader

            if (File.Exists(configureSettings))
            {
                
                FileStream fs = null;
                XmlTextReader xmlTxtReader = null;
                try
                {
					/*file exists then open it in order to read it.*/
                    fs = new FileStream(configureSettings, FileMode.Open);
                    xmlTxtReader = new XmlTextReader(fs);
                    xmlTxtReader.WhitespaceHandling = WhitespaceHandling.None;
                    xmlTxtReader.MoveToContent();

					/*check for the first node */
                    if (xmlTxtReader.Name != "WebServer")
                    {
                        Logger.Log("The Name 'WebServer' does not exist in the config file");
                        throw new ApplicationException("");
                    }
                    else
                    {
                        //move to the next node
                        xmlTxtReader.Read();
                        if (xmlTxtReader.NodeType == XmlNodeType.EndElement)
                        {
                            xmlTxtReader.Read();
                        }
                        if (xmlTxtReader.Name != "ServerInfo")
                        {
                            Logger.Log("The Name 'ServerInfo' does not exist in the config file");
                            throw new ApplicationException("");
                            
                        }
                        else
                        {
                            folder = xmlTxtReader.GetAttribute("Folder");
                            port = Convert.ToInt32(xmlTxtReader.GetAttribute("Port"));
                        }


                        xmlTxtReader.Read();
                        if (xmlTxtReader.NodeType == XmlNodeType.EndElement)
                        {
                            xmlTxtReader.Read();
                        }
                        if (xmlTxtReader.Name != "DefaultFile")
                        {
                            throw new ApplicationException("");
                        }
                        else
                        {
                            fileName = xmlTxtReader.GetAttribute("FileName");
                        }
                    }
                    
                    return true;
                }
                catch (Exception e )
                {
                   Logger.Log("Cannot read Configuration File" + e);
                   
                    //write the exception to a logger.
                }
                finally
                {
                    if (xmlTxtReader != null)
                    {
                        xmlTxtReader.Close();
                    }
                }

            }
            else
            {
                //write to the logger that the file does not exists
                Logger.Log("Configuration File Does not Exist");
            }
            return false;
        }

    }
}
