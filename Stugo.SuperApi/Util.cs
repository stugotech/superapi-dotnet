using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    internal static class Util
    {
        public static bool HasInterface<TInterface>(this Type t)
        {
            return t.HasInterface(typeof(TInterface));
        }


        public static bool HasInterface(this Type t, Type interfaceType)
        {
            return t.GetInterface(interfaceType) != null;
        }


        public static Type GetInterface(this Type t, Type interfaceType)
        {
            if (interfaceType.IsGenericTypeDefinition)
            {
                // http://stackoverflow.com/a/1121864
                if (t.IsInterface && t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType)
                    return t;

                return t
                    .GetInterfaces()
                    .FirstOrDefault((i) => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
            }
            else
            {
                return t.GetInterface(interfaceType.FullName);
            }
        }



        static HashSet<Type> numericTypes = new HashSet<Type>
        {
            typeof(byte), typeof(sbyte),
            typeof(ushort), typeof(uint), typeof(ulong),
            typeof(short), typeof(int), typeof(long),
            typeof(double), typeof(float), typeof(decimal)
        };

        public static bool IsNumeric(this Type t)
        {
            return numericTypes.Contains(t);
        }
    }
}
