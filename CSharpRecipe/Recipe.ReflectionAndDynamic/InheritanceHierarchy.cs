using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.ReflectionAndDynamic
{
    public class TypeHierarchy
    {
        public Type DerivedType { get; set; }
        public IEnumerable<Type> InheritanceChain { get; set; }
    }

    public static class InheritanceHierarchyExtension
    {
        public static IEnumerable<Type> GetInheritaceChain(this Type derivedType)
            => (from t in derivedType.GetBaseTypes() select t).Reverse();

        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Type current = type;
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }

        public static IEnumerable<TypeHierarchy> GetTypeHerarchies(this Assembly asm) =>
            from Type type in asm.GetTypes()
            select new TypeHierarchy
            {
                DerivedType = type,
                InheritanceChain = type.GetInheritaceChain()
            };

        public static IEnumerable<MemberInfo> GetMemberOverrides(this Type type)
            =>
                from ms in
                    type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
                                    BindingFlags.DeclaredOnly)
                where ms != ms.GetBaseDefinition()
                select ms.GetBaseDefinition();

        public static MethodInfo GetBaseMethodOverridden(this Type type, string methodName, Type[] paramTypes)
        {
            MethodInfo method = type.GetMethod(methodName, paramTypes);
            MethodInfo baseDef = method?.GetBaseDefinition();
            if (baseDef!= method)
            {
                bool foundMatch = (from p in baseDef.GetParameters()
                    join op in paramTypes
                        on p.ParameterType.UnderlyingSystemType equals op.UnderlyingSystemType
                    select p).Any();

                if (foundMatch)
                {
                    return baseDef;
                }
            }
            return null;
        }
    }

    public class TestDisplay
    {
        public static void DisplayInheritanceChain(IEnumerable<Type> chain)
        {
            StringBuilder buider = new StringBuilder();
            foreach (Type t in chain)
            {
                if (buider.Length==0)
                {
                    buider.Append(t.Name);
                }
                else
                {
                    buider.AppendFormat($"<-{t.Name}");
                }
            }

            Console.WriteLine($"Base Type List:{buider.ToString()}");
        }

        public static void DisplayTypeHierarchies(Assembly asm)
        {
            var typeHierarchies = asm.GetTypeHerarchies();
            foreach (var th in typeHierarchies)
            {
                Console.WriteLine($"Derived Type:{th.DerivedType.FullName}");
                DisplayInheritanceChain(th.InheritanceChain);
                Console.WriteLine();
            }
        }
    }
}
