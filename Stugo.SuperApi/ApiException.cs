using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class ApiException : Exception
    {
        public ApiErrorModel Error { get; set; }

        public ApiException()
            : base("There has been an error with the remote server")
        {
        }
    }
}
