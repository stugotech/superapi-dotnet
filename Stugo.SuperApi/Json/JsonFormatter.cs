using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
            else if (value is string)
            {
                return FormatString((string)value);
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return FormatDictionary((IDictionary)value);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return FormatArray((IEnumerable)value);
            }
            else
            {
                return FormatObject(value);
            }
        }


        private static Regex stringRegex = new Regex(@"[\x00-\x20\x7F-\x9F\\\/""]");
        private static MatchEvaluator stringEvaluator = new MatchEvaluator(EscapeControlChar);

        private static string EscapeControlChar(Match match)
        {
            char c = match.Value[0];

            switch (c)
            {
                case '\\':
                    return @"\\";

                case '/':
                    return @"\/";

                case '"':
                    return @"""";

                case '\b':
                    return @"\b";

                case '\f':
                    return @"\f";

                case '\n':
                    return @"\n";

                case '\r':
                    return @"\r";

                case '\t':
                    return @"\t";

                default:
                    return String.Format(@"\u{0:X4}", (int)c);
            }
        }
        
        private static string FormatString(string value)
        {
            return '"' + stringRegex.Replace(value, stringEvaluator) + '"';
        }


        private static string FormatDictionary(IDictionary value)
        {
            var builder = new StringBuilder();
            builder.Append("{");

            foreach (DictionaryEntry pair in value)
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


        private static string FormatArray(IEnumerable value)
        {
            var elements = new List<string>();

            foreach (var e in value)
            {
                elements.Add(FormatValue(e));
            }

            return '[' + string.Join(",", elements) + ']';
        }


        private static string FormatObject(object value)
        {
            var dictionary = value
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(x => x.Name, x => x.GetValue(value));

            return FormatDictionary(dictionary);
        }
    }
}
