using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recipe.ThreadAndConcurrent
{
    public class ThreadStaticField
    {
        [ThreadStatic]
        public static string bar = "Initilized string";

        public static void DisplayStaticFieldValue()
        {
            string msg = $"{Thread.CurrentThread.GetHashCode()} contains static field value of:{ThreadStaticField.bar}";
            Console.WriteLine(msg);
        }
    }

    public class TestClass
    {
        public static void TestStaticField()
        {
            ThreadStaticField.DisplayStaticFieldValue();

            Thread newThreadField = new Thread(ThreadStaticField.DisplayStaticFieldValue);
            newThreadField.Start();

            ThreadStaticField.DisplayStaticFieldValue();
        }
    }
}
