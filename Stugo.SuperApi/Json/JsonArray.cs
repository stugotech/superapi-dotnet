using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public class JsonArray : List<object>, IJsonType
    {

        public T As<T>()
        {
            return (T)As(typeof(T));
        }


        public object As(Type type)
        {
            Type interfaceType;

            if (type.IsAssignableFrom(typeof(JsonArray)))
            {
                return this;
            }
            else if (type.IsArray)
            {
                return AsArray(type.GetElementType());
            }
            else if (type.HasInterface<IList>() && (interfaceType = type.GetInterface(typeof(IList<>))) != null)
            {
                Type[] types = interfaceType.GetGenericArguments();
                return AsList(type, types[0]);
            }
            else
            {
                throw new JsonParseException("Could not convert");
            }
        }


        public object AsList(Type collectionType, Type elementType)
        {
            var list = (IList)Activator.CreateInstance(collectionType);

            foreach (var element in this)
            {
                list.Add(ConvertElement(element, elementType));
            }

            return list;
        }


        public object AsArray(Type elementType)
        {
            var array = Array.CreateInstance(elementType, this.Count);

            for (int i = 0; i < this.Count; i++)
            {
                array.SetValue(ConvertElement(this[i], elementType), i);
            }

            return array;
        }


        private object ConvertElement(object element, Type elementType)
        {
            if (elementType.IsPrimitive)
            {
                return Convert.ChangeType(element, elementType);
            }
            else if (element is IJsonType)
            {
                return ((IJsonType)element).As(elementType);
            }
            else
            {
                throw new JsonParseException("Could not convert");
            }
        }
    }
}
