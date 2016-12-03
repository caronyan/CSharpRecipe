using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DebugAndException
{
    public delegate int AsyncInvoke();

    public class TestAsyncInvoke
    {
        public static int Method1()
        {
            throw new Exception("Method1");
        }
    }

    public class AsyncDelegateException
    {               

        public void PollAsyncDelegate()
        {
            AsyncInvoke mi = new AsyncInvoke(TestAsyncInvoke.Method1);
            IAsyncResult ar = mi.BeginInvoke(null, null);

            while (!ar.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
                Console.WriteLine(".");
            }
            Console.WriteLine("Finish polling");

            try
            {
                int reVal = mi.EndInvoke(ar);
                Console.WriteLine("Reval (polling): " + reVal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());                
            }
        }


    }
}
