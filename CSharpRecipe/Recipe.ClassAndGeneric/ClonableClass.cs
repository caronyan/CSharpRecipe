using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Shallow copy interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IShallowCopy<T>
    {
        T ShallowCopy();
    }

    /// <summary>
    /// Deep copy interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeepCopy<T>
    {
        T DeepCopy();
    }

    /// <summary>
    /// Class that implements IShallowCopy<T>
    /// </summary>
    public class ShallowClone : IShallowCopy<ShallowClone>
    {
        public int Data;
        public List<string> StrList = new List<string>();
        public Object ObjData = new object();

        public ShallowClone ShallowCopy() => (ShallowClone) this.MemberwiseClone();
    }

    /// <summary>
    /// Class that implements IDeepCopy<T>
    /// </summary>
    [Serializable]
    public class DeepClone : IDeepCopy<DeepClone>
    {
        public int Data;
        public List<string> StrList = new List<string>();
        public Object ObjData = new object();

        public DeepClone DeepCopy()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();

            bf.Serialize(mStream, this);
            mStream.Flush();
            mStream.Position = 0;

            return (DeepClone) bf.Deserialize(mStream);
        }
    }

    /// <summary>
    /// MultiClone implementation
    /// </summary>
    [Serializable]
    public class MultiClone : IShallowCopy<MultiClone>, IDeepCopy<MultiClone>
    {
        public int Data;
        public List<string> StrList = new List<string>();
        public Object ObjData = new object();

        public MultiClone ShallowCopy() => (MultiClone) this.MemberwiseClone();

        public MultiClone DeepCopy()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();

            bf.Serialize(mStream, this);
            mStream.Flush();
            mStream.Position = 0;

            return (MultiClone)bf.Deserialize(mStream);
        }
    }
}
