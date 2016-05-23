using Stugo.SuperApi.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class ResourceClient
    {
        private readonly string baseUrl;
        private readonly string resourceName;
        private readonly ApiClient client;

        public ResourceClient(string baseUrl, string resourceName)
        {
            this.baseUrl = baseUrl;
            this.resourceName = resourceName;
            this.client = new ApiClient();
        }


        public async Task<Resource> List(RequestOptions options = null)
        {
            return await client.Get($"{baseUrl}/{resourceName}", options);
        }


        public async Task<Resource> Get(object id, RequestOptions options = null)
        {
            return await client.Get($"{baseUrl}/{resourceName}/{id}", options);
        }


        public async Task<Resource> Create(object data, RequestOptions options = null)
        {
            return await client.Create($"{baseUrl}/{resourceName}", data, options);
        }


        public async Task<Resource> Update(object id, object data, RequestOptions options = null)
        {
            return await client.Update($"{baseUrl}/{resourceName}/{id}", data, options);
        }


        public async Task<Resource> Replace(object id, object data, RequestOptions options = null)
        {
            return await client.Replace($"{baseUrl}/{resourceName}/{id}", data, options);
        }


        public async Task<Resource> Delete(object id, RequestOptions options = null)
        {
            return await client.Delete($"{baseUrl}/{resourceName}/{id}", options);
        }
    }
}
