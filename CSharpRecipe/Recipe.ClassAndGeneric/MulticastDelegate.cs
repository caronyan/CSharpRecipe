using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Test method group
    /// </summary>
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
            throw new Exception("This is a test exception");
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

            List<Exception> invocationExceptions = new List<Exception>();

            foreach (Func<int> instance in delegateList.EveryOther())
            {
                try
                {
                    int retVal = instance();
                    Console.WriteLine($"Delegate returns {retVal}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    invocationExceptions.Add(ex);
                }
               
            }

            if (invocationExceptions.Count>0)
            {
                throw new MulticastInvocationException(invocationExceptions);
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

    /// <summary>
    /// Exception handler class
    /// </summary>
    [Serializable]
    public class MulticastInvocationException : Exception
    {
        private List<Exception> _invocationExceptions;

        public MulticastInvocationException() : base()
        {

        }

        public MulticastInvocationException(IEnumerable<Exception> invocationExceptions)
        {
            this._invocationExceptions = new List<Exception>(invocationExceptions);
        }

        public MulticastInvocationException(string message):base(message)
        {

        }

        public MulticastInvocationException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected MulticastInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _invocationExceptions = (List<Exception>) info.GetValue("InvocationExceptions", typeof(List<Exception>));
        }

        [SecurityPermission(SecurityAction.Demand,SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InvocationExceptions", this.InvocationExceptions);
            base.GetObjectData(info, context);
        }

        public ReadOnlyCollection<Exception> InvocationExceptions
            => new ReadOnlyCollection<Exception>(_invocationExceptions);
    }
}
