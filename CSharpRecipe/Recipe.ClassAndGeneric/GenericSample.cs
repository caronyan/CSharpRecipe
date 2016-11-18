using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Sample generic dictionary like class implementation
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public sealed class GenericSample<T1, T2> : IEquatable<GenericSample<T1, T2>>
    {
        private static readonly IEqualityComparer<T1> FirstComparer = EqualityComparer<T1>.Default;
        private static readonly IEqualityComparer<T2> SecondComparer = EqualityComparer<T2>.Default;

        private readonly T1 first;
        private readonly T2 second;

        public GenericSample(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 First
        {
            get { return first; }
        }

        public T2 Second
        {
            get { return second; }
        }

        public bool Equals(GenericSample<T1, T2> other)
        {
            return other != null && FirstComparer.Equals(this.First, other.First) &&
                   SecondComparer.Equals(this.Second, other.Second);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GenericSample<T1, T2>);
        }

        public override int GetHashCode()
        {
            return FirstComparer.GetHashCode(first) * 37 + SecondComparer.GetHashCode(second);
        }
    }

    /// <summary>
    /// A generic collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedSizeCollection<T>
    {
        #region Properties

        public static int InstanceCount { get; set; }

        public int ItemCount { get; private set; }

        private T[] Items { get; set; }

        #endregion //Properties

        public FixedSizeCollection(int items)
        {
            FixedSizeCollection<T>.InstanceCount++;
            this.Items = new T[items];
        }

        public int AddItem(T item)
        {
            if (this.ItemCount < this.Items.Length)
            {
                this.Items[this.ItemCount] = item;
                return this.ItemCount++;
            }
            else
            {
                throw new Exception("Item queue is full");
            }
        }

        public T GetItem(int index)
        {
            if (index >= this.Items.Length && index >= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return this.Items[index];
        }
    }
}
