using System;
using System.Diagnostics;

namespace Common
{
    public class EventLogger
    {
        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static void Log(Exception ex, string source = null)
        {
            EventLog.WriteEntry(
                CheckSource(source), 
                ex.Message + "\n" + ex.StackTrace, 
                EventLogEntryType.Error);
        }

        /// <summary>
        /// Log info ~ 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void Log(string message, string source = null)
        {
            EventLog.WriteEntry(
                CheckSource(source), 
                message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="et"></param>
        /// <param name="category">
        /// Short range -32,768 to 32,767 can be used to identify logs from parts of the application
        /// This can be anything from the users Id to values representing classes withing the application:
        /// 
        /// ie: 1 = UserLogin; 2 = DebtorsCapture, 3 = CreditorsCapture ect
        /// </param>
        /// <param name="eventID"></param>
        /// <param name="source"></param>
        public static void Log(string message, EventLogEntryType et, short category, int eventID = 0, string source = null)
        {
            EventLog.WriteEntry(
                CheckSource(source),
                message,
                et,
                eventID,
                category);
        }

        /// <summary>
        /// Needs to be run with Admin privileges
        /// </summary>
        /// <param name="source"></param>
        /// <param name="logName"></param>
        public static void Create(string source, string logName)
        {
            var _source = CheckSource(source);
            var _logName = CheckSource(source);

            if (!EventLog.SourceExists(_source))
            {
                EventLog.CreateEventSource(_source, _logName);
            }
        }

        #region helpers
        private static string CheckSource(string source)
        {
            return source ?? "MyTestEventLog"; //read 'MyTestEventLog' from config
        }
        #endregion
    }
}
