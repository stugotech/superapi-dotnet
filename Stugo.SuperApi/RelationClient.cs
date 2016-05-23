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


        public async Task<Resource> Create(object data, RequestOptions options = null)
        {
            return await client.Create(url, data, options);
        }


        public async Task<Resource> Update(object data, RequestOptions options = null)
        {
            return await client.Update(url, data, options);
        }


        public async Task<Resource> Replace(object data, RequestOptions options = null)
        {
            return await client.Replace(url, data, options);
        }


        public async Task<Resource> Delete(RequestOptions options = null)
        {
            return await client.Delete(url, options);
        }
    }
}
