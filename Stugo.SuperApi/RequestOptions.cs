using Stugo.SuperApi.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugo.SuperApi
{
    public class RequestOptions
    {
        private readonly Dictionary<string, string> fields;
        private readonly Dictionary<string, string> filter;
        private readonly Dictionary<string, SortDirection> sort;
        private readonly HashSet<string> include;
        private Dictionary<string, string> paging;


        public RequestOptions()
        {
            fields = new Dictionary<string, string>();
            filter = new Dictionary<string, string>();
            sort = new Dictionary<string, SortDirection>();
            include = new HashSet<string>();
        }


        public RequestOptions Fields(string type, string[] fieldList)
        {
            fields[type] = string.Join(",", fieldList);
            return this;
        }


        public RequestOptions Filter(string field, params object[] values)
        {
            string value = String.Join(",", values.Select((x) => JsonFormatter.FormatValue(x)));
            filter[field] = value;
            return this;
        }


        public RequestOptions Sort(string field, SortDirection direction)
        {
            sort[field] = direction;
            return this;
        }


        public RequestOptions Include(string type)
        {
            include.Add(type);
            return this;
        }


        public RequestOptions PageByNumber(int number, int size)
        {
            paging = new Dictionary<string, string>()
            {
                { "number", number.ToString() },
                { "size", size.ToString() }
            };

            return this;
        }


        public RequestOptions PageByOffset(int offset, int size)
        {
            paging = new Dictionary<string, string>()
            {
                { "offset", offset.ToString() },
                { "size", size.ToString() }
            };

            return this;
        }


        public RequestOptions PageByAfter(object after, int size)
        {
            paging = new Dictionary<string, string>()
            {
                { "after", JsonFormatter.FormatValue(after) },
                { "size", size.ToString() }
            };

            return this;
        }


        public RequestOptions PageByBefore(object before, int size)
        {
            paging = new Dictionary<string, string>()
            {
                { "before", JsonFormatter.FormatValue(before) },
                { "size", size.ToString() }
            };

            return this;
        }


        public string ToQueryString()
        {
            var qs = new List<string>();
            AddRange(qs, "fields", fields);
            AddRange(qs, "filter", filter);
            AddRange(qs, "page", paging);
            qs.Add("sort=" + string.Join(",", sort.Select((pair) => (pair.Value == SortDirection.Descending ? "-" : "") + pair.Key)));
            qs.Add("include=" + string.Join(",", include));

            return string.Join("&", qs);
        }


        private void AddRange(List<string> qs, string name, Dictionary<string, string> keys)
        {
            qs.AddRange(keys.Select((pair) => $"{name}[{pair.Key}]={pair.Value}"));
        }
    }
}
