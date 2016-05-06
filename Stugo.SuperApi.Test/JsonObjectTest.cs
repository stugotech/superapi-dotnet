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
    public class JsonObjectTest
    {
        [Fact]
        public void AsJsonObjectTest()
        {
            var obj = new JsonObject();
            Assert.Same(obj, obj.As<JsonObject>());
        }


        [Fact]
        public void AsDictionaryTest()
        {
            var obj = new JsonObject()
            {
                { "a", 1 },
                { "b", 2 }
            };

            var dict = obj.As<Dictionary<string, int>>();
            Assert.Equal(1, dict["a"]);
            Assert.Equal(2, dict["b"]);
        }

        [Fact]
        public void AsSimpleClassTest()
        {
            var obj = new JsonObject()
            {
                { "foo", 1 },
                { "bar", false }
            };

            var c = obj.As<SimpleClass>();
            Assert.Equal(1, c.Foo);
            Assert.Equal(false, c.Bar);
        }


        [Fact]
        public void AsComplexClassTest()
        {
            var obj = new JsonObject()
            {
                { "simple", new JsonObject()
                    {
                        { "foo", 1 },
                        { "bar", false }
                    }
                },
                { "numbers", new JsonArray() { 1, 2 } }
            };

            var c = obj.As<ComplexClass>();
            Assert.Equal(1, c.Simple.Foo);
            Assert.Equal(false, c.Simple.Bar);
            Assert.Equal(new List<int>() { 1, 2 }, c.Numbers);
        }
    }

    public class SimpleClass
    {
        public int Foo { get; set; }
        public bool Bar { get; set; }
    }


    public class ComplexClass
    {
        public SimpleClass Simple { get; set; }

        public List<int> Numbers { get; set; }
    }
}
