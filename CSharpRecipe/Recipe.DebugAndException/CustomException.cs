using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DebugAndException
{
    [Serializable]
    public class CustomException:Exception,ISerializable
    {
        #region Constructors

        public CustomException() : base()
        {

        }

        public CustomException(string message) : base(message)
        {

        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public CustomException(string message, string serverName) : base(message)
        {
            this.ServerName = serverName;
        }

        #endregion

        #region Properties

        public string ServerName { get; }

        public override string Message => $"{base.Message}{Environment.NewLine}" +
                                          $"The server ({this.ServerName ?? "Unknown"})" +
                                          "has encountered an error.";
        #endregion

        #region Overridden Methods

        public override string ToString() => "An error has occured in a server component of this client." +
                                             $"{Environment.NewLine}Server Name:" +
                                             $"{this.ServerName}{Environment.NewLine}" +
                                             $"{this.ToFullDisplayString()}";

        [SecurityPermission(SecurityAction.LinkDemand,Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ServerName", this.ServerName);
        }

        #endregion

        public string ToBaseString() => base.ToString();
    }

    public static class ExceptionEntensions
    {
        public static string ToFullDisplayString(this Exception ex)
        {
            StringBuilder displayText = new StringBuilder();
            WriteExceptionDetail(displayText, ex);
            foreach (Exception inner in ex.GetNestedExceptionList())
            {
                displayText.AppendFormat("**** INNEREXCEPTION START ****{0}", Environment.NewLine);
                WriteExceptionDetail(displayText, inner);
                displayText.AppendFormat("**** INNEREXCEPTION END ****{0}{0}", Environment.NewLine);
            }
            return displayText.ToString();
        }        

        public static void WriteExceptionDetail(StringBuilder builder, Exception ex)
        {
            builder.AppendFormat("Message:{0}{1}", ex.Message, Environment.NewLine);
            builder.AppendFormat("Type:{0}{1}", ex.GetType(), Environment.NewLine);
            builder.AppendFormat("Source:{0}{1}", ex.Source, Environment.NewLine);
            builder.AppendFormat("HelpLink:{0}{1}", ex.HelpLink, Environment.NewLine);
            builder.AppendFormat("TargetSite:{0}{1}", ex.TargetSite, Environment.NewLine);
            builder.AppendFormat("Data:{0}", Environment.NewLine);
            foreach (DictionaryEntry de in ex.Data)
            {
                builder.AppendFormat("\t{0}:{1}{2}", de.Key, de.Value, Environment.NewLine);
            }
            builder.AppendFormat("StackTrace:{0}{1}", ex.StackTrace, Environment.NewLine);
        }
    }

}

