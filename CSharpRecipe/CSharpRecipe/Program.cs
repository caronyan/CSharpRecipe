using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipe.ClassAndGeneric;
using Recipe.DebugAndException;

namespace CSharpRecipe
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Test IEnumerable<T>
            //IEnumerable<int> iterable = EnumeratorTest.CreateEnumerable();
            //IEnumerator<int> iterator = iterable.GetEnumerator();
            //Console.WriteLine("Start to iterate");

            //while (true)
            //{
            //    Console.WriteLine("Calling MoveNext()...");
            //    bool result = iterator.MoveNext();
            //    Console.WriteLine($"...MoveNext result={result}");

            //    if (!result)
            //    {
            //        break;
            //    }

            //    Console.WriteLine("Fetching Current...");
            //    Console.WriteLine($"...Current result={iterator.Current}");
            //}
            #endregion

            #region Test Reflection Exception

            //ReflectException.ReflectionException();
            #endregion

            #region Async Delegate Exception

            //AsyncDelegateException ade = new AsyncDelegateException();
            //ade.PollAsyncDelegate();

            #endregion

            #region Process State Check

            //var processes = Process.GetProcesses().ToArray();
            //foreach (var proc in processes)
            //{
            //    var state = ProcessRespondingCheck.GetProcessState(proc);

            //    switch (state)
            //    {
            //            case ProcessRespondingState.Responding:
            //                Console.WriteLine($"{proc.ProcessName} is responding.");
            //            break;
            //            case ProcessRespondingState.NotResponding:
            //            Console.WriteLine($"{proc.ProcessName} is not responding.");
            //            break;
            //        case ProcessRespondingState.UnKnown:
            //            Console.WriteLine($"{proc.ProcessName} is unknown.");                        
            //            break;
            //    }
            //}

            #endregion

            #region Debugger Display Test

            TestDebuggerDisplay td = new TestDebuggerDisplay();
            td.TestDisplay();

            #endregion

            Console.ReadKey();
        }
    }
}
