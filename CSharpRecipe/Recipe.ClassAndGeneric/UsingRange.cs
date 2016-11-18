using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ClassAndGeneric
{
    /// <summary>
    /// Using [using] to dispose temp object automatically
    /// </summary>
    public class UsingRange
    {
        public void WriteSomething()
        {
            using (FileStream fs = new FileStream("Text.txt", FileMode.Create))
            {
                fs.WriteByte((byte)1);
                fs.WriteByte((byte)2);
                fs.WriteByte((byte)3);

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("some text");
                }
            }
        }
    }
}
