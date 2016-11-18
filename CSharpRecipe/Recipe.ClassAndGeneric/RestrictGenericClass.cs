using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class DisposableList<T> : IList<T> where T : class, IDisposable
    {
        private List<T> _items = new List<T>();

        private void Delete(T item) => item.Dispose();

        //IList<T> members
        public int IndexOf(T item) => _items.IndexOf(item);

        public void Insert(int index, T item) => _items.Insert(index, item);

        public T this[int index]
        {
            get { return (_items[index]); }
            set { _items[index] = value; }
        }

        public void RemoveAt(int index)
        {
            Delete(this[index]);
            _items.RemoveAt(index);
        }

        //ICollection members
        public void Add(T item) => _items.Add(item);

        public bool Contains(T item) => _items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        //IEnumerable<T> members
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        //IEnumerable members
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        //Other members
        public void Clear()
        {
            for (int index = 0; index < _items.Count; index++)
            {
                Delete(_items[index]);
            }

            _items.Clear();
        }

        public bool Remove(T item)
        {
            int index = _items.IndexOf(item);

            if (index>=0)
            {
                Delete(_items[index]);
                _items.RemoveAt(index);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
