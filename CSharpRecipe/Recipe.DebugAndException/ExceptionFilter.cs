using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DebugAndException
{
    public class ExceptionFilter
    {
        public void ProtectedCallTheDatabase(string problem)
        {
            try
            {
                CallTheDatabase(problem);
                Console.WriteLine("********");
            }
            catch (DatabaseException dex) when (dex.Number == -2)
            {
                Console.WriteLine("********");                
            }
        }

        public void CallTheDatabase(string problem)
        {

        }
    }

    [Serializable]
    public class DatabaseException : DbException
    {
        public DatabaseException(string message) : base(message)
        {
        }

        public byte Class { get; set; }
        public Guid ClientConnectionId { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SqlErrorCollection Errors { get; set; }
        public int LineNumber { get; set; }
        public int Number { get; set; }
        public string Procedure { get; set; }
        public string Server { get; set; }
        public override string Source => base.Source;
        public byte State { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
