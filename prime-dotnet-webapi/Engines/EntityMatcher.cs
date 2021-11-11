using System;
using System.Collections.Generic;
using System.Linq;

namespace Prime.Engines
{
    public class EntityMatcher<TExisting, TIncoming, TKey>
        where TExisting : class
        where TIncoming : class
        where TKey : IComparable<TKey>
    {
        public class MatchingResult
        {
            public ICollection<(TExisting existing, TIncoming incoming)> Updated { get; set; } = new List<(TExisting existing, TIncoming incoming)>();
            public ICollection<TExisting> Dropped { get; set; }
            public ICollection<TIncoming> Added { get; set; } = new List<TIncoming>();
        }

        private readonly Func<TExisting, TKey> _existingKeySelector;
        private readonly Func<TIncoming, TKey> _incomingKeySelector;

        public EntityMatcher(Func<TExisting, TKey> existingKeySelector, Func<TIncoming, TKey> incomingKeySelector)
        {
            _existingKeySelector = existingKeySelector;
            _incomingKeySelector = incomingKeySelector;
        }

        public MatchingResult Match(IEnumerable<TExisting> existingEntities, IEnumerable<TIncoming> incomingObjects)
        {
            var result = new MatchingResult();
            var existingDict = existingEntities.ToDictionary(x => _existingKeySelector.Invoke(x), x => x);

            foreach (var incoming in incomingObjects)
            {
                var key = _incomingKeySelector.Invoke(incoming);

                if (existingDict.TryGetValue(key, out var existing))
                {
                    result.Updated.Add(new(existing, incoming));
                    existingDict.Remove(key);
                }
                else
                {
                    result.Added.Add(incoming);
                }
            }

            result.Dropped = existingDict.Values;

            return result;
        }
    }

    public class EntityMatcher
    {
        public static EntityMatcher<TExisting, TIncoming, TKey> MatchUsing<TExisting, TIncoming, TKey>(Func<TExisting, TKey> existingKeySelector, Func<TIncoming, TKey> incomingKeySelector) where TExisting : class where TIncoming : class where TKey : IComparable<TKey>
        {
            return new EntityMatcher<TExisting, TIncoming, TKey>(existingKeySelector, incomingKeySelector);
        }

        public static EntityMatcher<TExisting, TExisting, TKey> MatchUsing<TExisting, TKey>(Func<TExisting, TKey> matchingTypeKeySelector) where TExisting : class where TKey : IComparable<TKey>
        {
            return MatchUsing(matchingTypeKeySelector, matchingTypeKeySelector);
        }
    }
}
