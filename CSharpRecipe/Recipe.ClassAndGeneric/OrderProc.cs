using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class OrderByTest
    {
        public static SortedList<int, string> Data = new SortedList<int, string>()
        {
            { 2,"two"},
            { 5,"five"},
            { 3,"three"},
            { 1,"one"},
        };

        public void SortByLinq()
        {            
            //SortedList default
            Console.WriteLine("SortedList default:\n");
            foreach (KeyValuePair<int, string> kvp in Data)
            {                
                Console.WriteLine($"\t{kvp.Key}\t{kvp.Value}\n");
            }

            //Linq sorted
            var query = from d in Data
                        orderby d.Key descending
                        select d;

            Console.WriteLine("Linq sorted:\n");
            foreach (KeyValuePair<int, string> kvp in query)
            {                
                Console.WriteLine($"\t{kvp.Key}\t{kvp.Value}\n");
            }            
        }
    }
}
