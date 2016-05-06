using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi.Json
{
    public interface IJsonType
    {
        object As(Type type);
        T As<T>();
    }
}
