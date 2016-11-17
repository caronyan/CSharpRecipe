using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Using StructLayout and FieldOffset to implement a union type
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    struct SignedNumberWithText
    {
        [FieldOffset(0)]
        public sbyte Num1;
        [FieldOffset(0)]
        public short Num2;
        [FieldOffset(0)]
        public int Num3;
        [FieldOffset(0)]
        public float Num4;
        [FieldOffset(0)]
        public long Num5;
        [FieldOffset(0)]
        public double Num6;
        [FieldOffset(16)]
        public string Text1;
    }
}
