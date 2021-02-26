using System;
using System.Linq;

namespace PrimeTests
{
    public static class ObjectExtensions
    {
        public static bool AllPropertiesNull<T>(this T obj)
        {
            return AllPropertiesNullExcept(obj, Array.Empty<string>());
        }

        public static bool AllPropertiesNullExcept<T>(this T obj, params string[] excludedProperties)
        {
            return typeof(T).GetProperties()
                .Where(p => !excludedProperties.Contains(p.Name))
                .All(p => p.GetValue(obj) == null);
        }

        /// <summary>
        /// Returns an arbitrary new string that is different from the original string.
        /// Returns empty string on null.
        /// </summary>
        /// <param name="original"></param>
        public static string Bump(this string original)
        {
            if (original == null)
            {
                return "";
            }

            return original + "a";
        }
    }
}
