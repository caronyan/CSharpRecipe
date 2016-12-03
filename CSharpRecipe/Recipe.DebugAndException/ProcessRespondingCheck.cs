using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DebugAndException
{
    public enum ProcessRespondingState
    {
        Responding,
        NotResponding,
        UnKnown
    }

    public class ProcessRespondingCheck
    {
        public static ProcessRespondingState GetProcessState(Process p)
        {
            if (p.MainWindowHandle==IntPtr.Zero)
            {
                Trace.WriteLine($"{p.ProcessName} does not have aMainWindowHandle");
                return ProcessRespondingState.UnKnown;
            }
            else
            {
                if (!p.Responding)
                {
                    return ProcessRespondingState.NotResponding;
                }
                else
                {
                    return ProcessRespondingState.Responding;
                }
            }
        }
    }
}
