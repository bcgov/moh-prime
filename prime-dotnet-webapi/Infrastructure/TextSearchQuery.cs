using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using LinqKit;

namespace Prime.Infrastructure
{
    public class TextSearchQuery<T>
    {
        private IQueryable<T> _originalQuery;
        private IEnumerable<Expression<Func<T, string>>> _propertySelectors;
        private IEnumerable<Expression<Func<T, IEnumerable<string>>>> _collectionSelectors;

        public TextSearchQuery(IQueryable<T> originalQuery, IEnumerable<Expression<Func<T, string>>> propertySelectors, IEnumerable<Expression<Func<T, IEnumerable<string>>>> collectionSelectors)
        {
            _originalQuery = originalQuery;
            _propertySelectors = propertySelectors ?? Enumerable.Empty<Expression<Func<T, string>>>();
            _collectionSelectors = collectionSelectors ?? Enumerable.Empty<Expression<Func<T, IEnumerable<string>>>>();
        }

        public TextSearchQuery<T> Search(params Expression<Func<T, string>>[] propertySelectors)
        {
            _propertySelectors = _propertySelectors.Concat(propertySelectors);
            return this;
        }

        public TextSearchQuery<T> SearchCollections(params Expression<Func<T, IEnumerable<string>>>[] collectionSelectors)
        {
            _collectionSelectors = _collectionSelectors.Concat(collectionSelectors);
            return this;
        }

        public IQueryable<T> Containing(string term)
        {
            term = term.ToLower();
            var predicate = PredicateBuilder.New<T>();

            foreach (var selector in _propertySelectors)
            {
                predicate.Or(x => selector.Invoke(x).ToLower().Contains(term));
            }

            foreach (var selector in _collectionSelectors)
            {
                predicate.Or(x => selector.Invoke(x).Any(s => s.ToLower().Contains(term)));
            }

            return _originalQuery.AsExpandable().Where(predicate);
        }
    }
}
