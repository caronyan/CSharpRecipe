using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Recipe.DebugAndException
{
    public class AsyncExceptionHandling
    {
        public async Task TestHandlingAsyncExceptionAsync()
        {
            try
            {
                await ACreateSomeCodeAsync();
            }
            catch (DefectCreatedException dce)
            {

                Console.WriteLine($"A introduced a Defect:{dce.Message}");
            }

            Task bCode = BCreateSomeCodeAsync();
            Task cCode = CCreateSomeCodeAsync();
            Task dCode = DCreateSomeCodeAsync();
            Task teamCompleteTask = Task.WhenAll(new Task[] { bCode, cCode, dCode });
            try
            {
                await teamCompleteTask;
            }
            catch
            {
                var defectMessages = teamCompleteTask.Exception?.InnerExceptions.Select(e => e.Message).ToList();
                defectMessages?.ForEach(m => Console.WriteLine($"{m}"));
            }

            try
            {
                try
                {
                    await ACreateSomeCodeAsync();
                }
                catch (DefectCreatedException dce)
                {
                    Console.WriteLine(dce.ToString());
                    await WriteEventLogEntryAsync("ManagerApplication", dce.Message, EventLogEntryType.Error);
                    throw;
                }
            }
            catch (DefectCreatedException dce)
            {
                Console.WriteLine(dce.ToString());
            }
        }

        public async Task WriteEventLogEntryAsync(string source, string message, EventLogEntryType type)
        {
            await Task.Factory.StartNew(() => EventLog.WriteEntry(source, message, type));
        }

        public async Task ACreateSomeCodeAsync()
        {
            Random rnd = new Random();
            await Task.Delay(rnd.Next(100, 1000));
            throw new DefectCreatedException("Null Reference", 42);
        }

        public async Task BCreateSomeCodeAsync()
        {
            Random rnd = new Random();
            await Task.Delay(rnd.Next(100, 1000));
            throw new DefectCreatedException("Ambiguous Match", 2);
        }

        public async Task CCreateSomeCodeAsync()
        {
            Random rnd = new Random();
            await Task.Delay(rnd.Next(100, 1000));
            throw new DefectCreatedException("Quota Exceeded", 11);
        }

        public async Task DCreateSomeCodeAsync()
        {
            Random rnd = new Random();
            await Task.Delay(rnd.Next(100, 1000));
            throw new DefectCreatedException("Out Of Memory", 8);
        }
    }

    public class DefectCreatedException : Exception
    {
        public DefectCreatedException() : base()
        {
        }

        public DefectCreatedException(string message) : base(message)
        {
        }

        public DefectCreatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DefectCreatedException(string defect, int line) : base(string.Empty)
        {
            this.Defect = defect;
            this.Line = line;
        }

        public DefectCreatedException(string defect, int line, Exception innerException)
            : base(string.Empty, innerException)
        {
            this.Defect = defect;
            this.Line = line;
        }

        public DefectCreatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Defect { get; }
        public int Line { get; }

        public override string ToString() => $"{Environment.NewLine}{this.ToFullDisplayString()}";

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Defect", this.Defect);
            info.AddValue("Line", this.Line);
        }

        public string ToBaseString() => base.ToString();
    }
}
