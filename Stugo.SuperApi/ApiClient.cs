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
    public class ApiClient
    {
        public async Task<Resource> Get(string url, RequestOptions options = null)
        {
            return await Request("GET", url, null, options);
        }


        public async Task<Resource> Create(string url, object data, RequestOptions options = null)
        {
            return await Request("POST", url, data, options);
        }


        public async Task<Resource> Update(string url, object data, RequestOptions options = null)
        {
            return await Request("PATCH", url, data, options);
        }


        public async Task<Resource> Replace(string url, object data, RequestOptions options = null)
        {
            return await Request("PUT", url, data, options);
        }


        public async Task<Resource> Delete(string url, RequestOptions options = null)
        {
            return await Request("DELETE", url, null, options);
        }


        async Task<Resource> Request(string method, string url, object data, RequestOptions options)
        {

            if (options != null)
            {
                var qs = options.ToQueryString();

                if (!String.IsNullOrEmpty(qs))
                {
                    url += '?' + qs;
                }
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;

            if (data != null)
            {
                var bytes = Encoding.UTF8.GetBytes(JsonFormatter.FormatValue(data));
                request.ContentType = "application/json";
                request.ContentLength = bytes.Length;

                Stream stream = await request.GetRequestStreamAsync();
                await stream.WriteAsync(bytes, 0, bytes.Length);
                await stream.FlushAsync();
                stream.Close();
            }

            var webResponse = await request.GetResponseAsync();
            string json;

            using (var reader = new StreamReader(webResponse.GetResponseStream()))
            {
                json = await reader.ReadToEndAsync();
            }

            var parser = new JsonParser(json);
            return parser.ParseObject().As<Resource>();
        }
    }
}
