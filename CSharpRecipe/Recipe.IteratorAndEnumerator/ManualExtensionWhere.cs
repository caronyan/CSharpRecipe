using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.IteratorAndEnumerator
{
    public static class ManualExtensionWhere
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null || predicate == null)
            {
                throw new ArgumentException();
            }

            return WhereImpl(source, predicate);
        }

        private static IEnumerable<T> WhereImpl<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
