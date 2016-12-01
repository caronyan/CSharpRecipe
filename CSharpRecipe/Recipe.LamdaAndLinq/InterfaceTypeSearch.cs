using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.LamdaAndLinq
{
    public class InterfaceSearch
    {
        public void SearchInterfaces()
        {
            Type[] interfaces =
            {
                typeof(System.ICloneable),
                typeof(System.Collections.ICollection),
                typeof(System.IAppDomainSetup)
            };

            Type searchType = typeof(System.Collections.ArrayList);

            var matches = from t in searchType.GetInterfaces()
                join s in interfaces on t equals s
                select s;

            Console.WriteLine("Matches found.");
            foreach (Type item in matches)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
