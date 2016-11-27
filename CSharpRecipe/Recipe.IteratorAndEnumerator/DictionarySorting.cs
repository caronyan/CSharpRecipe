using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.IteratorAndEnumerator
{
    public class DictionarySorting
    {
        public Dictionary<int, string> Hash = new Dictionary<int, string>()
        {
            { 2,"two"},
            { 1,"one"},
            { 5,"five"},
            { 4,"four"},
            { 3,"three"}
        };

        public void SortingByLinq()
        {
            var x = from k in Hash.Keys
                orderby k ascending
                select k;

            foreach (var k in x)
            {
                Console.WriteLine($"key:{k},Value:{Hash[k]}");
            }
        }
    }
}
