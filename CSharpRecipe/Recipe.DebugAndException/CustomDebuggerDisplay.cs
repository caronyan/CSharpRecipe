using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recipe.DebugAndException
{
    [DebuggerDisplay("{First}-{Middle}-{Last}")]
    public class SampleEntity
    {
        public SampleEntity(string first, string middle, string last)
        {
            this.First = first;
            this.Middle = middle;
            this.Last = last;
        }

        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }

    public class TestDebuggerDisplay
    {
        public void TestDisplay()
        {
            SampleEntity se=new SampleEntity("1","2","3");     
            Thread.Sleep(200);       
        }
    }

}
