using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    public class MultiReturns
    {
        public void Test()
        {
            int height;
            int width;
            int depth;

            ReturnDimensions(1, out height, out width, out depth);
            Dimension dim = ReturnDimension(1);
            Tuple<int, int, int> objTuple = ReturnDimensionAsTuple(1);

        }

        public struct Dimension
        {
            public int height;
            public int width;
            public int depth;
        }

        /// <summary>
        /// Using [out] keyword to return results
        /// </summary>
        /// <param name="inputShape"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        public void ReturnDimensions(int inputShape, out int height, out int width, out int depth)
        {
            // Must be initialized before use
            height = 0;
            width = 0;
            depth = 0;
        }

        /// <summary>
        /// Using struct to return results
        /// </summary>
        /// <param name="inputShape"></param>
        /// <returns></returns>
        public Dimension ReturnDimension(int inputShape)
        {
            Dimension objDim = new Dimension();

            return objDim;
        }

        /// <summary>
        /// Using tuple to return results
        /// </summary>
        /// <param name="inputShape"></param>
        /// <returns></returns>
        public Tuple<int, int, int> ReturnDimensionAsTuple(int inputShape)
        {
            var objDim = Tuple.Create<int, int, int>(0, 0, 0);
            return objDim;
        }
    }
}
