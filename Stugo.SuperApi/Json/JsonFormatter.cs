using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public class JsonFormatter
    {
        public static string FormatValue(object value)
        {
            var type = value.GetType();

            if (type.IsNumeric())
            {
                return value.ToString();
            }
            else if (value is bool)
            {
                return (bool)value ? "true" : "false";
            }
            else if (value is DateTime)
            {
                return '"' + ((DateTime)value).ToString("o") + '"';
            }
            else if (value is String)
            {
                return '"' + value.ToString() + '"';
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                var builder = new StringBuilder();
                builder.Append("{");

                foreach (DictionaryEntry pair in (IDictionary)value)
                {
                    var key = (string)pair.Key;
                    builder.Append('"');
                    builder.Append(Char.ToLower(key[0]));
                    builder.Append(key.Substring(1));
                    builder.Append('"');
                    builder.Append(":");
                    builder.Append(FormatValue(pair.Value));
                    builder.Append(",");
                }

                if (builder.Length > 1)
                {
                    builder.Length -= 1;
                }

                builder.Append("}");
                return builder.ToString();
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var elements = new List<string>();

                foreach (var e in (IEnumerable)value)
                {
                    elements.Add(FormatValue(e));
                }

                return '[' + string.Join(",", elements) + ']';
            }
            else
            {
                var dictionary = value
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(x => x.Name, x => x.GetValue(value));

                return FormatValue(dictionary);
            }
        }
    }
}
