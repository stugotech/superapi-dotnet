using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public class JsonParser
    {
        private readonly string json;
        private int index;
        private StringBuilder builder;

        public JsonParser(string json)
        {
            this.json = json;
            builder = new StringBuilder();
        }


        public JsonObject ParseObject()
        {
            var obj = new JsonObject();
            ExpectChar('{');
            char c;

            if (PeekChar() != '}')
            {
                do
                {
                    var key = ParseString();
                    ExpectChar(':');
                    var value = ParseValue();

                    obj.Add(key, value);
                    c = SkipChar(',');
                }
                while (c == ',');
            }

            ExpectChar('}');
            return obj;
        }


        public JsonArray ParseArray()
        {
            var list = new JsonArray();
            ExpectChar('[');
            char c;

            if (PeekChar() != ']')
            {
                do
                {
                    list.Add(ParseValue());
                    c = SkipChar(',');
                }
                while (c == ',');
            }

            ExpectChar(']');
            return list;
        }


        public object ParseValue()
        {
            char c = PeekChar();

            if (Char.IsDigit(c) || c == '-')
            {
                return ParseNumber();
            }
            else if (c == '"')
            {
                return ParseString();
            }
            else if (c == '[')
            {
                return ParseArray();
            }
            else if (c == '{')
            {
                return ParseObject();
            }
            else if (index + 4 <= json.Length && json.Substring(index, 4) == "true")
            {
                index += 4;
                return true;
            }
            else if (index + 5 <= json.Length && json.Substring(index, 5) == "false")
            {
                index += 5;
                return false;
            }
            else if (index + 4 <= json.Length && json.Substring(index, 4) == "null")
            {
                index += 4;
                return null;
            }
            else
            {
                throw new JsonParseException("Unexpected character " + c);
            }
        }


        public object ParseNumber()
        {
            int start = index;
            char c;

            while (index < json.Length &&
                (Char.IsDigit(c = json[index]) || c == 'e' || c == 'E' || c == '+' || c == '-' || c == '.'))
            {
                index++;
            }

            double d = double.Parse(json.Substring(start, index - start));
            int i = (int)d;

            if (Math.Abs(d - i) < double.Epsilon)
                return i;
            else
                return d;
        }


        public string ParseString()
        {
            ExpectChar('"');
            int start = index;
            char c;

            builder.Length = 0;

            while ((c = NextChar(false)) != '"')
            {
                if (c == '\\')
                {
                    builder.Append(json.Substring(start, index - start - 1));

                    switch (c = NextChar(false))
                    {
                        case '"':
                        case '\\':
                        case '/':
                            builder.Append(c);
                            break;

                        case 'b':
                            builder.Append('\b');
                            break;

                        case 'f':
                            builder.Append('\f');
                            break;

                        case 'n':
                            builder.Append('\n');
                            break;

                        case 'r':
                            builder.Append('\r');
                            break;

                        case 't':
                            builder.Append('\t');
                            break;

                        case 'u':
                            uint code = (ParseHexDigit() << 12) + (ParseHexDigit() << 8) + (ParseHexDigit() << 4) + ParseHexDigit();
                            builder.Append((char)code);
                            break;

                        default:
                            throw new JsonParseException("Unknown escape char " + c);
                    }

                    start = index;
                }
            }

            builder.Append(json.Substring(start, index - start - 1));
            return builder.ToString();
        }


        private uint ParseHexDigit()
        {
            char c = NextChar(false);

            if (c >= '0' && c <= '9')
            {
                return (uint)c - '0';
            }
            else if (c >= 'A' && c <= 'F')
            {
                return (uint)c - 'A' + 10;
            }
            else if (c >= 'a' && c <= 'f')
            {
                return (uint)c - 'a' + 10;
            }
            else
            {
                throw new JsonParseException("Unexpected character " + c);
            }
        }


        private char NextChar(bool skipWhite = true)
        {
            char c = default(char);
            
            if (skipWhite)
            {
                    while (index < json.Length && Char.IsWhiteSpace(c = json[index]))
                    {
                        index++;
                    }
            }

            if (index == json.Length)
                throw new JsonParseException("Unexpected end of input");

            return json[index++];
        }


        private char PeekChar(bool skipWhite = true)
        {
            char c = NextChar(skipWhite);
            index--;
            return c;
        }


        private char SkipChar(char skip, bool skipWhite = true)
        {
            char c = NextChar(skipWhite);

            if (c != skip)
            {
                index--;
            }

            return c;
        }


        private void ExpectChar(char expect, bool skipWhite = true)
        {
            char c = NextChar(skipWhite);

            if (c != expect)
            {
                throw new JsonParseException("Expected " + expect);
            }
        }
    }
}
