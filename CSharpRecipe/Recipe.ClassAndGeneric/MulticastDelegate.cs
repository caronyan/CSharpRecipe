using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class TestInvokeIntReturn
    {
        public static int Method1()
        {
            Console.WriteLine("Invoked Method1\n");
            return 1;
        }

        public static int Method2()
        {
            Console.WriteLine("Invoked Method2\n");
            return 2;
        }

        public static int Method3()
        {
            Console.WriteLine("Invoked Method3\n");
            return 3;
        }
    }

    public static class TestFunc
    {        
        public static void InvokeWithTest()
        {
            Func<int> myDelegateInstance1 = TestInvokeIntReturn.Method1;
            Func<int> myDelegateInstance2 = TestInvokeIntReturn.Method2;
            Func<int> myDelegateInstance3 = TestInvokeIntReturn.Method3;

            Func<int> allInstances = myDelegateInstance1 + myDelegateInstance2 + myDelegateInstance3;

            Console.WriteLine("Fire delegates in reverse");
            Delegate[] delegateList = allInstances.GetInvocationList();
            foreach (Func<int> instance in delegateList.EveryOther())
            {
               int retVal= instance();
                Console.WriteLine($"Delegate returns {retVal}\n");
            }
        }

        /// <summary>
        /// Extention method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        static IEnumerable<T> EveryOther<T>(this IEnumerable<T> enumerable)
        {
            bool reNext = true;
            foreach (T t in enumerable)
            {
                if (reNext)
                {
                    yield return t;
                }
                reNext = !reNext;
            }
        }
    }
}
