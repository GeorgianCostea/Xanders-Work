/*
 * File Name    : HTTPStructs.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Contains the request / response structs and 
 * the enums for the request / respnse.
 */



using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Globalization;
using System.Web;

namespace Web_Server
{


    enum RequestState
    {
        METHOD, URL, URLPARM, URLVALUE, VERSION,
        HEADERKEY, HEADERVALUE, BODY, OK
    };

    enum ResponseState
    {
        OK = 200,
        BAD_REQUEST = 400,
        NOT_FOUND = 404,
        SERVER_ERROR = 500

    };

    public struct RequestStruct
    {
        public string method;
        public string version;
        public string url;
        public Hashtable arguments;
        public Hashtable headers;
        public int sizeOfBody;
        public byte[] DataInBody;
        public bool execute;
    }


    public struct ResponseStruct
    {
        public int status;
        public string version;
        public Hashtable headers;
        public int sizeOfBody;
        public byte[] DataInBody;
        public System.IO.FileStream newFileStream;
    }

}
