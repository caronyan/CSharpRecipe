using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class ExpressionTreeTest
    {
        public void ExpressionTest()
        {
            Expression<Func<string, string, bool>> expression = (x, y) => x.StartsWith(y);

            var compiled = expression.Compile();

            Console.WriteLine(compiled("First", "Fir"));
        }

        public void ExpressionTestDetail()
        {
            MethodInfo method = typeof(string).GetMethod("StartWith", new[] {typeof(string)});
            var target = Expression.Parameter(typeof(string), "x");
            var methodArg = Expression.Parameter(typeof(string), "y");
            Expression[] methodArgs = new[] {methodArg};

            Expression call = Expression.Call(target, method, methodArgs);

            var lambdaParameters = new[] { target, methodArg};
            var lambda = Expression.Lambda<Func<string, string, bool>>(call, lambdaParameters);

            var compiled = lambda.Compile();

            Console.WriteLine(compiled("First", "Fir"));
        }
    }
}
