using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ReflectionAndDynamic
{
    public class ListAssemblies
    {
        public static void BuildDependentAssemblyList(string path, StringCollection assemblies)
        {
            if (assemblies == null)
            {
                assemblies = new StringCollection();
            }

            if (assemblies.Contains(path) == true)
            {
                return;
            }

            try
            {
                Assembly asm = null;

                if (path.IndexOf(@"\", 0, path.Length, StringComparison.Ordinal) != -1 ||
                    path.IndexOf("/", 0, path.Length, StringComparison.Ordinal) != -1)
                {
                    asm = Assembly.LoadFrom(path);
                }
                else
                {
                    asm = Assembly.Load(path);
                }

                if (asm != null)
                {
                    assemblies.Add(path);
                }

                AssemblyName[] imports = asm.GetReferencedAssemblies();

                foreach (AssemblyName asmName in imports)
                {
                    BuildDependentAssemblyList(asmName.FullName, assemblies);
                }
            }
            catch (FileLoadException fle)
            {
                Console.WriteLine(fle);
            }
        }


    }
}
