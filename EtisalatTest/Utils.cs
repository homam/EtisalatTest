using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EtisalatTest
{
    public static class Utils
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }

        public static Header[] ToHeaders(System.Collections.Specialized.NameValueCollection headers)
        {
            return headers.AllKeys.Select(k => new Header(k, headers.GetValues(k))).ToArray();
        }
    }
}