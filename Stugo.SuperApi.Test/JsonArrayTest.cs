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
    public class JsonArrayTest
    {
        [Fact]
        public void AsJsonArrayTest()
        {
            var obj = new JsonArray();
            Assert.Same(obj, obj.As<JsonArray>());
        }


        [Fact]
        public void AsListTest()
        {
            var array = new JsonArray() { 1, 2 };
            var list = array.As<List<int>>();
            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
        }


        [Fact]
        public void AsArrayTest()
        {
            var array = new JsonArray() { 1, 2 };
            var a = array.As<int[]>();
            Assert.Equal(2, a.Length);
            Assert.Equal(new int[] { 1, 2 }, a);
        }
    }
}
