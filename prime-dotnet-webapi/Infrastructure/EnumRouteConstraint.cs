using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Prime.Infrastructure
{
    public class EnumRouteConstraint<T> : IRouteConstraint where T : struct
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string candidate = values[routeKey]?.ToString();

            if (string.IsNullOrWhiteSpace(candidate))
            {
                return false;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return converter?.ConvertFromInvariantString(null, candidate) != null;
        }
    }
}
