using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace Prime.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<K, V> RemoveNullValues<K, V>(this Dictionary<K, V> dictionary)
        {
            return dictionary
                .Where(x => x.Value != null)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public static string ToQueryStringUrl(this Dictionary<string, string> parameters, string baseUrl, bool removeNullValues = true)
        {
            if (removeNullValues)
            {
                parameters = parameters.RemoveNullValues();
            }

            return QueryHelpers.AddQueryString(baseUrl, parameters);
        }
    }
}
