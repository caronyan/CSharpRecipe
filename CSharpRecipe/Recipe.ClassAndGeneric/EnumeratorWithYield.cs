using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class EnumeratorTest
    {
        public static readonly string Padding = new string(' ', 30);

        public static IEnumerable<int> CreateEnumerable()
        {
            Console.WriteLine($"{Padding} start of CreateEnumerable() ");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{Padding} about to yield {i}");
                yield return i;
                Console.WriteLine($"{Padding} after yield");
            }

            Console.WriteLine($"{Padding} tielding final value");
            yield return -1;

            Console.WriteLine($"{Padding} end of CreateEnumerable()");
        }
    }
}
