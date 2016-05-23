using Stugo.SuperApi.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stugo.SuperApi.Test
{
    public class JsonFormatterTest
    {
        [Fact]
        public void IntTest()
        {
            Assert.Equal("123", JsonFormatter.FormatValue(123));
        }

        [Fact]
        public void DoubleTest()
        {
            Assert.Equal("3.14", JsonFormatter.FormatValue(3.14));
        }

        [Fact]
        public void DecimalTest()
        {
            Assert.Equal("3.14", JsonFormatter.FormatValue(3.14m));
        }

        [Fact]
        public void TrueTest()
        {
            Assert.Equal("true", JsonFormatter.FormatValue(true));
        }

        [Fact]
        public void FalseTest()
        {
            Assert.Equal("false", JsonFormatter.FormatValue(false));
        }

        [Fact]
        public void DateTimeTest()
        {
            Assert.Equal("\"2016-12-25T12:31:00.0000000Z\"", JsonFormatter.FormatValue(new DateTime(2016, 12, 25, 12, 31, 0, 0, DateTimeKind.Utc)));
        }

        [Fact]
        public void StringTest()
        {
            Assert.Equal("\"hello\"", JsonFormatter.FormatValue("hello"));
        }

        [Fact]
        public void ArrayTest()
        {
            Assert.Equal("[1,2,3]", JsonFormatter.FormatValue(new int[] { 1, 2, 3 }));
        }

        [Fact]
        public void DictionaryTest()
        {
            Assert.Equal("{\"a\":1,\"b\":2}", JsonFormatter.FormatValue(new Dictionary<string, int>() { { "a", 1 }, { "b", 2 } }));
        }

        [Fact]
        public void ObjectTest()
        {
            Assert.Equal("{\"a\":1,\"b\":2}", JsonFormatter.FormatValue(new { a = 1, b = 2 }));
        }

        [Fact]
        public void UpperCaseObjectTest()
        {
            Assert.Equal("{\"a\":1,\"bar\":2}", JsonFormatter.FormatValue(new { A = 1, Bar = 2 }));
        }
    }
}
