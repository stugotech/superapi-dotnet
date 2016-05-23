using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public class JsonObject : Dictionary<string, object>, IJsonType
    {
        public JsonObject()
        {
        }


        public JsonObject(IDictionary<string, object> dictionary)
            : base(dictionary)
        {
        }


        public T As<T>()
        {
            return (T)As(typeof(T));
        }


        public object As(Type type)
        {
            Type interfaceType;

            if (type.IsAssignableFrom(typeof(JsonObject)))
            {
                return this;
            }
            else if (type.HasInterface<IDictionary>() && (interfaceType = type.GetInterface(typeof(IDictionary<,>))) != null)
            {
                Type[] types = interfaceType.GetGenericArguments();
                return AsDictionary(type, types[0], types[1]); 
            }
            else
            {
                return AsObject(type);
            }
        }


        protected object AsDictionary(Type collectionType, Type keyType, Type valueType)
        {
            var dictionary = (IDictionary)Activator.CreateInstance(collectionType);

            foreach (var pair in this)
            {
                var key = Convert.ChangeType(pair.Key, keyType);
                object value;

                if (valueType.IsPrimitive)
                {
                    value = Convert.ChangeType(pair.Value, valueType);
                }
                else if (pair.Value is IJsonType)
                {
                    value = ((IJsonType)pair.Value).As(valueType);
                }
                else
                {
                    throw new JsonParseException("Could not convert");
                }

                dictionary.Add(key, value);
            }

            return dictionary;
        }


        protected object AsObject(Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var pair in this)
            {
                var member = type.GetProperty(pair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

                if (member != null)
                {
                    if (pair.Value == null)
                    {
                        member.SetValue(obj, null);
                    }
                    else if (member.PropertyType.IsPrimitive)
                    {
                        member.SetValue(obj, Convert.ChangeType(pair.Value, member.PropertyType));
                    }
                    else if (pair.Value is IJsonType)
                    {
                        member.SetValue(obj, ((IJsonType)pair.Value).As(member.PropertyType));
                    }
                    else
                    {
                        throw new JsonParseException("Could not convert");
                    }
                }
            }

            return obj;
        }


        {
            if (this.ContainsKey("error"))
            {
                throw this.As<ApiException>();
            }
        }
    }
}
