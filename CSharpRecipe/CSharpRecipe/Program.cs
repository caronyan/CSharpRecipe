using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipe.ClassAndGeneric;

namespace CSharpRecipe
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderByTest test = new OrderByTest();
            test.SortByLinq();


            Console.ReadKey();
        }
    }
}
