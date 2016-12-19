using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recipe.ThreadAndConcurrent
{
    public static class EventRaiseExtentions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, object sender,
            ref EventHandler<TEventArgs> eventDelegate)
        {
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
