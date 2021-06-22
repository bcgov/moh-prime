using System.Linq;
using System.Collections.Generic;

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
    }
}
