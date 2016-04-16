using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace TheGildedRose.Logging
{
    public class Logger
    {
        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "WriteErrorMessage"     simply writes an error message to the event log.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="message"></param>
        public static void WriteErrorMessage(string message)
        {
            string source = "Application";
            string log = "Application";

            if (!EventLog.SourceExists(source)) EventLog.CreateEventSource(source, log);
            EventLog.WriteEntry(source, message, EventLogEntryType.Error);

        }
    }
}