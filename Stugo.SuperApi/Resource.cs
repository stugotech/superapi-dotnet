using Stugo.SuperApi.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class Resource
    {
        public IDictionary<string, string> Links { get; set; }
        public JsonObject Meta { get; set; }
        public JsonObject Attributes { get; set; }
        public List<Resource> Elements { get; set; }
        public List<Resource> Includes { get; set; }


        public RelationClient Relation(string name)
        {
            string url = Links[name];
            Resource includes = null;
            
            if (Includes != null)
            {
                includes = Includes.FirstOrDefault(x => x.Links["$self"] == url);
            }

            return new RelationClient(url, includes);
        }


        public void ThrowIfError()
        {
            Attributes.ThrowIfError();
        }
    }
}
