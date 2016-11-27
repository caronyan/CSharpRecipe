using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.IteratorAndEnumerator
{
    static class ListExtension
    {
        public static IEnumerable<T> GetAll<T>(this List<T> myList, T searchValue) => myList.Where(t => t.Equals(searchValue));

        /// <summary>
        /// Using binary search for finding out duplicated elements
        /// For sorted list only
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myList"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
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

    /// <summary>
    /// Keep list sorted
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedList<T> : List<T>
    {
        public new void Add(T item)
        {
            int position = this.BinarySearch(item);
            if (position<0)
            {
                position = ~position;
            }

            this.Insert(position, item);
        }

        public void ModifiedSorted(T item, int index)
        {
            this.RemoveAt(index);

            int position = this.BinarySearch(item);
            if (position<0)
            {
                position = ~position;
            }

            this.Insert(position, item);
        }
    }
}
