using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ReflectionAndDynamic
{
    public class LocalVariableTest
    {
        public static IReadOnlyCollection<LocalVariableInfo> GetLocalVars(string asmPath, string typeName,
            string methodName)
        {
            Assembly asm = Assembly.LoadFrom(asmPath);
            Type asmType = asm.GetType(typeName);

            MethodInfo mi = asmType.GetMethod(methodName);
            MethodBody mb = mi.GetMethodBody();

            ReadOnlyCollection<LocalVariableInfo> vars = (ReadOnlyCollection<LocalVariableInfo>) mb.LocalVariables;

            return vars;
        }
    }
}
