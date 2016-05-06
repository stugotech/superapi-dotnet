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


        public async Task<Resource> List(RequestOptions options)
        {
            return await client.Get($"{baseUrl}/{resourceName}", options);
        }


        public async Task<Resource> Get(RequestOptions options, object id)
        {
            return await client.Get($"{baseUrl}/{resourceName}/{id}", options);
        }


        public async Task<Resource> Create(RequestOptions options, object data)
        {
            return await client.Create($"{baseUrl}/{resourceName}", options, data);
        }


        public async Task<Resource> Update(RequestOptions options, object id, object data)
        {
            return await client.Update($"{baseUrl}/{resourceName}/{id}", options, data);
        }


        public async Task<Resource> Replace(RequestOptions options, object id, object data)
        {
            return await client.Replace($"{baseUrl}/{resourceName}/{id}", options, data);
        }


        public async Task<Resource> Delete(RequestOptions options, object id)
        {
            return await client.Delete($"{baseUrl}/{resourceName}/{id}", options);
        }
    }
}
