using System;
using System.Collections.Generic;
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

            ReflectException.ReflectionException();
            #endregion 


            Console.ReadKey();
        }
    }
}
