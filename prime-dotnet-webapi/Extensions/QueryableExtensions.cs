using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

using Prime.Infrastructure;
using Prime.Models;

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

        public static TextSearchQuery<T> Search<T>(this IQueryable<T> source, params Expression<Func<T, string>>[] propertySelectors)
        {
            return new TextSearchQuery<T>(source, propertySelectors, null);
        }

        public static TextSearchQuery<T> SearchCollections<T>(this IQueryable<T> source, params Expression<Func<T, IEnumerable<string>>>[] collectionSelectors)
        {
            return new TextSearchQuery<T>(source, null, collectionSelectors);
        }

        public static IQueryable<Party> WithPartyType(this IQueryable<Party> source, PartyType withType)
        {
            return source.Where(party => party.PartyEnrolments
                .Any(pe => pe.PartyType == withType));
        }
    }
}
