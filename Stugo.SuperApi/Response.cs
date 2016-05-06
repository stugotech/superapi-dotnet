using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class Response
    {
        private readonly IDictionary<string, object> dictionary;


        public Response(IDictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }


        
    }
}
