using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class RelationClient
    {
        private readonly string url;
        private readonly ApiClient client;
        private readonly Resource includes;


        public RelationClient(string url, Resource includes)
        {
            this.url = url;
            this.client = new ApiClient();
            this.includes = includes;
        }


        public async Task<Resource> Get(RequestOptions options)
        {
            if (includes != null)
            {
                return includes;
            }
            else
            {
                return await client.Get(url, options);
            }
        }


        public async Task<Resource> Create(RequestOptions options, object data)
        {
            return await client.Create(url, options, data);
        }


        public async Task<Resource> Update(RequestOptions options, object data)
        {
            return await client.Update(url, options, data);
        }


        public async Task<Resource> Replace(RequestOptions options, object data)
        {
            return await client.Replace(url, options, data);
        }


        public async Task<Resource> Delete(RequestOptions options)
        {
            return await client.Delete(url, options);
        }
    }
}
