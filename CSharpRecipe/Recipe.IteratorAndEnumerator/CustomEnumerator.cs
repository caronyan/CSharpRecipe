using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.IteratorAndEnumerator
{
    public class Container<T> : IEnumerable<T>
    {
        public Container()
        {
        }

        private List<T> _internalList = new List<T>();

        public IEnumerator<T> GetEnumerator() => _internalList.GetEnumerator();

        public IEnumerable<T> GetReverseOrderEnumerator()
        {
            foreach (T item in ((IEnumerable<T>)_internalList).Reverse())
            {
                yield return item;
            }
        }

        public IEnumerable<T> GetForwardStepEnumerator(int step)
        {
            foreach (T item in _internalList.EveryNthItem(step))
            {
                yield return item;
            }
        }

        public IEnumerable<T> GetReverseStepEnumerator(int step)
        {
            foreach (T item in ((IEnumerable<T>)_internalList).Reverse().EveryNthItem(step))
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Clear()
        {
            _internalList.Clear();
        }

        public void Add(T item)
        {
            _internalList.Add(item);
        }

        public void AddRange(ICollection<T> collection)
        {
            _internalList.AddRange(collection);
        }

    }

    /// <summary>
    /// Extension that return items every few steps
    /// </summary>
    public static class EnumerableExtension
    {
        public static IEnumerable<T> EveryNthItem<T>(this IEnumerable<T> enumerable, int step)
        {
            int current = 0;
            foreach (T item in enumerable)
            {
                ++current;
                if (current % step == 0)
                {
                    yield return item;
                }
            }
        }
    }
}
