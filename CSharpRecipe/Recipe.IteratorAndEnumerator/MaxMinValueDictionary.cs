using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.IteratorAndEnumerator
{
    [Serializable]
    public class MaxMinValueDictionary<T, TU> where TU : IComparable<TU>
    {
        protected Dictionary<T, TU> InternalDictionary = null;

        public MaxMinValueDictionary(TU maxValue, TU minValue)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            InternalDictionary = new Dictionary<T, TU>();
        }

        public TU MaxValue { get; private set; } = default(TU);
        public TU MinValue { get; private set; } = default(TU);

        public int Count => InternalDictionary.Count;

        public Dictionary<T, TU>.KeyCollection Keys => InternalDictionary.Keys;

        public Dictionary<T, TU>.ValueCollection Values => InternalDictionary.Values;

        public TU this[T key]
        {
            get { return InternalDictionary[key]; }
            set
            {
                if (value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0)
                {
                    InternalDictionary[key] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be within the range {MinValue} to {MaxValue}");
                }
            }
        }

        public void Add(T key, TU value)
        {
            if (value.CompareTo(MinValue) >= 0 && value.CompareTo(MaxValue) <= 0)
            {
                InternalDictionary.Add(key, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be within the range {MinValue} to {MaxValue}");
            }
        }

        public bool ContainsKey(T key) => InternalDictionary.ContainsKey(key);

        public bool ContainsValue(TU value) => InternalDictionary.ContainsValue(value);

        public override bool Equals(object obj) => InternalDictionary.Equals(obj);

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            InternalDictionary.GetObjectData(info, context);
        }

        public IEnumerator GetEnumerator() => InternalDictionary.GetEnumerator();

        public override int GetHashCode() => InternalDictionary.GetHashCode();

        public void OnDeserialization(object sender)
        {
            InternalDictionary.OnDeserialization(sender);
        }

        public override string ToString() => InternalDictionary.ToString();

        public bool TryGetValue(T key, out TU value) => InternalDictionary.TryGetValue(key, out value);

        public void Remove(T key)
        {
            InternalDictionary.Remove(key);
        }

        public void Clear()
        {
            InternalDictionary.Clear();
        }
    }
}
