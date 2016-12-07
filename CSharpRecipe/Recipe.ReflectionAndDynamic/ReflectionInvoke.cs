
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Recipe.ReflectionAndDynamic
{
    public class ReflectionInvoke
    {
        public static void ReflectionInvokeTest(XDocument xdoc, string asmPath)
        {
            //Test source using XML
            var test = from t in xdoc.Elements("Test")
                       select new
                       {
                           typeName = (string)t.Attribute("className").Value,
                           methodName = (string)t.Attribute("memberName").Value,
                           parameter = from p in t.Elements("Parameter") select new { arg = p.Value }
                       };

            Assembly asm = Assembly.LoadFrom(asmPath);

            foreach (var elem in test)
            {
                //Get class
                Type refClassType = asm.GetType(elem.typeName, true, false);

                //Try creating instance
                object refObj = Activator.CreateInstance(refClassType);
                if (refObj != null) //Instance created successfully
                {
                    //Get method
                    MethodInfo invokedMethod = refClassType.GetMethod(elem.methodName);
                    if (invokedMethod != null) //Method found
                    {
                        object[] arguments = new object[elem.parameter.Count()];
                        int index = 0;

                        foreach (var arg in elem.parameter)
                        {
                            //Get param type
                            Type paramType = invokedMethod.GetParameters()[index].ParameterType;
                            //Convert param type
                            arguments[index] = Convert.ChangeType(arg.arg, paramType);
                            index++;
                        }

                        object retObj = invokedMethod.Invoke(refObj, arguments);

                        Console.WriteLine($"\tReturned object:{retObj}");
                        Console.WriteLine($"\tReturned object:{retObj.GetType().FullName}");
                    }
                }
            }
        }
    }
}
