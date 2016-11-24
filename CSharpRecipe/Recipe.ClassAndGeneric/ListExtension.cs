using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    static class ListExtension
    {
        public static IEnumerable<T> GetAll<T>(this List<T> myList, T searchValue) => myList.Where(t => t.Equals(searchValue));

        public static T[] BinarySearchGetAll<T>(this List<T> myList, T searchValue)
        {
            List<T> retObjs = new List<T>();

            int center = myList.BinarySearch(searchValue);
            if (center > 0)
            {
                retObjs.Add(myList[center]);

                int left = center;
                while (left > 0 && myList[left - 1].Equals(searchValue))
                {
                    left -= 1;
                    retObjs.Add(myList[center]);
                }

                int right = center;
                while (right < (myList.Count - 1) && myList[right + 1].Equals(searchValue))
                {
                    right += 1;
                    retObjs.Add(myList[center]);
                }
            }

            return retObjs.ToArray();
        }

        public static int CountAll<T>(this List<T> myList, T searchValue) => myList.GetAll(searchValue).Count();

        public static int BinarySearchCountAll<T>(this List<T> myList, T searchValue)
            => BinarySearchGetAll(myList, searchValue).Count();
    }
}
