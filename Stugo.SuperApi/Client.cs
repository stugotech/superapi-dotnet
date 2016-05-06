using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class Client
    {
        private string baseUrl;

        public Client(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }


        public ResourceClient Resource(string name)
        {
            return new ResourceClient(baseUrl, name);
        }
    }
}
