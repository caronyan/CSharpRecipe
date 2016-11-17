using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Simple class that implements IComparable<>
    /// </summary>
    public class Square : IComparable<Square>
    {
        public Square()
        {
        }

        public Square(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height { get; set; }
        public int Width { get; set; }

        public int CompareTo(object obj)
        {
            Square square = obj as Square;
            if (square != null)
            {
                return CompareTo(square);
            }
            throw
                new ArgumentException("Both objects being compared must be of type Square");
        }

        public override string ToString() => ($"Height:{this.Height}  Width:{this.Width}");

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Square square = obj as Square;
            if (square != null)
            {
                return this.Height == square.Height;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Height.GetHashCode() | this.Width.GetHashCode();
        }

        public static bool operator ==(Square x, Square y) => x.Equals(y);
        public static bool operator !=(Square x, Square y) => !(x == y);
        public static bool operator >(Square x, Square y) => (x.CompareTo(y) < 0);
        public static bool operator <(Square x, Square y) => (x.CompareTo(y) > 0);


        public int CompareTo(Square other)
        {
            long area1 = this.Height * this.Width;
            long area2 = other.Height * other.Width;

            if (area1 == area2)
            {
                return 0;
            }
            else if (area1 > area2)
            {
                return 1;
            }
            else if (area2 > area1)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }
    }

    /// <summary>
    /// Comparer implementation
    /// </summary>
    public class CompareHeight : IComparer<Square>
    {
        public int Compare(object firstSquare, object secondSquare)
        {
            Square square1 = firstSquare as Square;
            Square square2 = secondSquare as Square;
            if (square1 == null || square2 == null)
            {
                throw new ArgumentException("Both params must be of type Square");
            }
            else
            {
                return Compare(square1, square2);
            }
        }

        public int Compare(Square x, Square y)
        {
            if (x.Height == y.Height)
            {
                return 0;
            }
            else if (x.Height > y.Height)
            {
                return 1;
            }
            else if (y.Height > x.Height)
            {
                return -1;
            }
            else
            {
                return -1;
            }
        }
    }
}
