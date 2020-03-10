using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ILookupService
    {
        Task<LookupEntity> GetLookupsAsync();

        Task<List<T>> GetLookupsAsync<TKey, T>(params Expression<Func<T, object>>[] includes) where T : class, ILookup<TKey>;
    }
}
