using System.Linq;
using Microsoft.AspNetCore.Http;


namespace Prime
{
    public static class HttpRequestExtensions
    {
        public static string GetHeader(this HttpRequest request, string name)
        {
            if (request.Headers.TryGetValue(name, out var value))
            {
                return value.SingleOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
