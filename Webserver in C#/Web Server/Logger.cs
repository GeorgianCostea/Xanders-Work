/*
 * File Name    : Logger.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Contains the logger class(static class) which logs all event
 * to a windows system logging file.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Web_Server
{
	/// <summary>
	///  The logging class will be used to record all events that take place
	/// in the system, so that in the event of data loss we will be able to recreate
	/// everything that has happened.
	/// </summary>
    public static class Logger
    {

		/// <summary>
		/// This method creates a new eventlog if it does not exist and than it logs the 
		/// event.
		/// </summary>
		/// <param name="message"></param>
        public static void Log(string message)
        {
            EventLog serviceEventLog = new EventLog("");
            if (!EventLog.SourceExists("MyEventSource"))
            {
                EventLog.CreateEventSource("MyEventSource", "MyEventLog");
                
            }
            serviceEventLog.Source = "MyEventSource";
            serviceEventLog.Log = "MyEventLog";
            serviceEventLog.WriteEntry(message);
        }

    }
}
