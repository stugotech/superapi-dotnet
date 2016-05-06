using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public class JsonParseException : Exception
    {
        public JsonParseException(string message)
            : base(message)
        {
        }
    }
}
