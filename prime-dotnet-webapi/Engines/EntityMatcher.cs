using System;
using System.Collections.Generic;
using System.Linq;

namespace Prime.Engines
{
    public class EntityMatcher<TExisting, TIncoming, TKey> where TExisting : class where TIncoming : class where TKey : struct
    {
        public class MatchingResult
        {
            public ICollection<(TExisting existing, TIncoming incoming)> Updated { get; set; } = new List<(TExisting existing, TIncoming incoming)>();
            public ICollection<TExisting> Dropped { get; set; } = new List<TExisting>();
            public ICollection<TIncoming> Added { get; set; }
        }

        private readonly Func<TExisting, TKey> _existingKeySelector;
        private readonly Func<TIncoming, TKey> _incomingKeySelector;

        public EntityMatcher(Func<TExisting, TKey> existingKeySelector, Func<TIncoming, TKey> incomingKeySelector)
        {
            _existingKeySelector = existingKeySelector;
            _incomingKeySelector = incomingKeySelector;
        }

        public MatchingResult Sort(IEnumerable<TExisting> existingEntities, IEnumerable<TIncoming> incoming)
        {
            var result = new MatchingResult();
            var incomingDictionary = incoming.ToDictionary(x => _incomingKeySelector.Invoke(x), x => x);

            foreach (var entity in existingEntities)
            {
                var key = _existingKeySelector.Invoke(entity);

                if (incomingDictionary.TryGetValue(key, out var value))
                {
                    result.Updated.Add(new(entity, value));
                    incomingDictionary.Remove(key);
                }
                else
                {
                    result.Dropped.Add(entity);
                }
            }

            result.Added = incomingDictionary.Values;

            return result;
        }
    }

    public class EntityMatcher
    {
        public static EntityMatcher<TExisting, TIncoming, TKey> MatchingOn<TExisting, TIncoming, TKey>(Func<TExisting, TKey> existingKeySelector, Func<TIncoming, TKey> incomingKeySelector) where TExisting : class where TIncoming : class where TKey : struct
        {
            return new EntityMatcher<TExisting, TIncoming, TKey>(existingKeySelector, incomingKeySelector);
        }
    }
}
