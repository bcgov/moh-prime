using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Prime.Services;

namespace PrimeTests.Mocks
{
    public class LookupServiceMock : BaseMockService, ILookupService
    {
        public LookupServiceMock() : base()
        { }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class for lookups
        }

        Task<List<T>> ILookupService.GetLookupsAsync<TKey, T>(params Expression<Func<T, object>>[] includes)
        {
            var type = typeof(T);
            var holder = this.GetHolder<short, T>();
            var results = new List<T>();
            foreach (var value in holder.Values.ToList())
            {
                results.Add((T)value);
            }

            return Task.FromResult(results);
        }
    }
}