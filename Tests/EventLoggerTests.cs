using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Diagnostics;

namespace Tests
{
    /// <summary>
    /// Log some dummy event logs.
    /// These can then be exported from windows 'Event Viewer' (to be imported by 'EvtxImporter')
    /// Then read from 'LogReader' (which reads from the relational database)
    /// 
    /// This then simulates a situation where the developer doesnt have access to the host OS and the '.evtx' files are provided by OPS 
    /// </summary>
    [TestClass]
    public class EventLoggerTests
    {
        [TestMethod]
        public void LogMessage()
        {
            EventLogger.Log("This is my informational message");
        }

        [TestMethod]
        public void LogError()
        {
            EventLogger.Log(new Exception("Exception message"));
        }

        [TestMethod]
        public void LogWithSource()
        {
            short category = (short)CategoryModel.Categorys.UserLogin; //1 Example UserLogin class
            for (int i = 0; i < 10; i++)
            {
                if (i == 5) //simulate an 'Exception' for 5
                {
                    EventLogger.Log(
                        "Exception for iteration " + i,
                        EventLogEntryType.Error,
                        category);
                }
                else
                {
                    EventLogger.Log(
                        "Information for iteration " + i,
                        EventLogEntryType.Information,
                        category);
                }
            }

            category = (short)CategoryModel.Categorys.DebtorsCapture; //2 Example DebtorsCapture class
            for (int i = 0; i < 10; i++)
            {
                EventLogger.Log(
                    "Debtors Capture info for iteration " + i,
                    EventLogEntryType.Information,
                    category);
            }

            category = 1; //UserLogin
            for (int i = 0; i < 5; i++)
            {
                EventLogger.Log(
                    "Exceptions at " + i,
                    EventLogEntryType.Error,
                    category);
            }

            category = (short)CategoryModel.Categorys.CreditorsCapture; //3 CreditorsCapture
            for (int i = 0; i < 5; i++)
            {
                EventLogger.Log(
                    "Warning at " + i,
                    EventLogEntryType.Warning,
                    category);
            }
        }
    }
}
