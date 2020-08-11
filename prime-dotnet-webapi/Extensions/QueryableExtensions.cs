using System;
using System.Linq;

namespace Prime
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Conditionally applys a new condition to the query.
        /// </summary>
        public static IQueryable<T> If<T>(this IQueryable<T> source, bool condition, Func<IQueryable<T>, IQueryable<T>> branch)
        {
            return condition ? branch(source) : source;
        }
    }
}
