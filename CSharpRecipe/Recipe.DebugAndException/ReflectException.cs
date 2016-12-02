using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Recipe.DebugAndException
{
    public static class ReflectException
    {
        public static void ReflectionException()
        {
            Type reflectedClass = typeof(ReflectException);

            try
            {
                MethodInfo methodToInvoke = reflectedClass.GetMethod("TestInvoke");
                methodToInvoke?.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToShortDisplayString());
            }
        }

        public static void TestInvoke()
        {
            throw new Exception("Thrown from invoked method.");
        }
    }

    public static class ExceptionDisplayExtention
    {
        public static string ToShortDisplayString(this Exception ex)
        {
            StringBuilder displayText = new StringBuilder();
            WriteExceptionShortDetail(displayText, ex);
            foreach (Exception inner in ex.GetNestedExceptionList())
            {
                displayText.AppendFormat("**** INNEREXCEPTION START ****{0}", Environment.NewLine);
                WriteExceptionShortDetail(displayText, inner);
                displayText.AppendFormat("**** INNEREXCEPTION END ****{0}{0}", Environment.NewLine);
            }
            return displayText.ToString();
        }

        public static IEnumerable<Exception> GetNestedExceptionList(this Exception ex)
        {
            Exception current = ex;
            do
            {
                current = current.InnerException;
                if (current != null)
                {
                    yield return current;
                }
            } while (current != null);
        }

        public static void WriteExceptionShortDetail(StringBuilder builder, Exception ex)
        {
            builder.AppendFormat("Message:{0}{1}", ex.Message, Environment.NewLine);
            builder.AppendFormat("Type:{0}{1}", ex.GetType(), Environment.NewLine);
            builder.AppendFormat("Source:{0}{1}", ex.Source, Environment.NewLine);
            builder.AppendFormat("TargetSite:{0}{1}", ex.TargetSite, Environment.NewLine);
        }
    }
}
