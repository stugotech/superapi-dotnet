using Stugo.SuperApi;
using Stugo.SuperApi.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MethaMeasure.Api.Test
{
    
    public class ParserTest
    {
        
        [Fact]
        public void ParseIntTest()
        {
            var parser = new JsonParser("532");
            var number = parser.ParseNumber();

            Assert.IsType<int>(number);
            Assert.Equal(532, number);
        }


        [Fact]
        public void ParseFloatTest()
        {
            var parser = new JsonParser("3.1415");
            var number = parser.ParseNumber();

            Assert.IsType<double>(number);
            Assert.Equal(3.1415, number);
        }


        [Fact]
        public void ParseStringTest()
        {
            var parser = new JsonParser(@"""hello, world\r\n""");
            var str = parser.ParseString();

            Assert.IsType<string>(str);
            Assert.Equal("hello, world\r\n", str);
        }


        [Fact]
        public void ParseBooleanTest()
        {
            var parser = new JsonParser("true");
            var b = parser.ParseValue();

            Assert.IsType<bool>(b);
            Assert.Equal(true, b);

            parser = new JsonParser("false");
            b = parser.ParseValue();

            Assert.IsType<bool>(b);
            Assert.Equal(false, b);
        }


        [Fact]
        public void ParseArrayTest()
        {
            var parser = new JsonParser("[1, true, [3, 4]]");
            var array = parser.ParseArray();

            Assert.Equal(new JsonArray() { 1, true, new JsonArray() { 3, 4 } }, array);
        }


        [Fact]
        public void ParseObjectTest()
        {
            var parser = new JsonParser("{\"a\": 1, \"b\": true, \"c\": {\"d\": 3, \"e\": [4]}}");
            var obj = parser.ParseObject();

            Assert.Equal(new JsonObject()
            {
                { "a", 1 },
                { "b", true },
                { "c", new JsonObject()
                    {
                        { "d",  3 },
                        { "e", new JsonArray() { 4 } }
                    }
                }
            }, obj);
        }
    }
}
