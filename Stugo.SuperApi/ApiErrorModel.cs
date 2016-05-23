using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class ApiErrorModel
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public Dictionary<string, object> Meta { get; set; }        
    }
}
