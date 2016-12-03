using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace Recipe.DebugAndException
{
    public enum EventIdType
    {
        NA = 0,
        Read = 1,
        Write = 2,
        ExceptionThrown = 3,
        BufferOverflowCondition = 4,
        SecurityFailure = 5,
        SecurityPotentiallyCompromised = 6
    }

    public enum CategoryType : short
    {
        None = 0,
        WriteToDb = 1,
        ReadFromDb = 2,
        WriteToFile = 3,
        ReadFromFile = 4,
        AppStartUp = 5,
        AppShutDown = 6,
        UserInput = 7
    }

    public class CustomEventLog
    {
        const string LocalMachine = ".";

        public CustomEventLog(string logName)
            : this(logName, Process.GetCurrentProcess().ProcessName)
        {

        }

        public CustomEventLog(string logName, string source)
            : this(logName, source, LocalMachine)
        {

        }

        public CustomEventLog(string logName, string source, string machineName = LocalMachine)
        {
            this.LogName = logName;
            this.SourceName = source;
            this.MachineName = machineName;

            Log = new EventLog(LogName, SourceName, MachineName);
        }

        private EventLog Log { get; set; } = null;
        public string LogName { get; set; }
        public string SourceName { get; set; }
        public string MachineName { get; set; } = LocalMachine;

        public void WriteToLog(string message, EventLogEntryType type, CategoryType category, EventIdType eventId)
        {
            if (Log == null)
            {
                throw new ArgumentNullException(nameof(Log), "This event log has not been opened or has been closed.");
            }

            EventLogPermission evtPermission = new EventLogPermission(EventLogPermissionAccess.Write, MachineName);
            evtPermission.Demand();

            Log.WriteEntry(message, type, (int)eventId, (short)category);

        }

        public void WriteToLog(string message, EventLogEntryType type, CategoryType category, EventIdType eventId,
            byte[] rawData)
        {
            if (Log == null)
            {
                throw new ArgumentNullException(nameof(Log), "This event log has not been opened or has been closed.");
            }

            EventLogPermission evtPermission = new EventLogPermission(EventLogPermissionAccess.Write, MachineName);
            evtPermission.Demand();

            Log.WriteEntry(message, type, (int)eventId, (short)category, rawData);
        }

        public IEnumerable<EventLogEntry> GetEntries()
        {
            EventLogPermission evtPermission = new EventLogPermission(EventLogPermissionAccess.Administer, MachineName);
            evtPermission.Demand();

            return Log?.Entries.Cast<EventLogEntry>().Where(evt => evt.Source == SourceName);
        }

        public void Clearlog()
        {
            EventLogPermission evtPermission = new EventLogPermission(EventLogPermissionAccess.Administer, MachineName);
            evtPermission.Demand();
            if (!IsNonCustomLog())
            {
                Log?.Clear();
            }
        }

        public void CloseLog()
        {
            Log?.Close();
            Log = null;
        }

        public void DeleteLog()
        {
            if (!IsNonCustomLog())
            {
                if (EventLog.Exists(LogName, MachineName))
                {
                    EventLog.Delete(LogName, MachineName);
                }
            }

            CloseLog();
        }

        public bool IsNonCustomLog()
        {
            if (LogName == string.Empty ||
                LogName == "Application" ||
                LogName == "Security" ||
                LogName == "Setup" ||
                LogName == "System")
            {
                return true;
            }

            return false;
        }


    }
}
