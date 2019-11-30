using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultLookupService : BaseService, ILookupService
    {
        public DefaultLookupService(
            ApiDbContext context, IHttpContextAccessor httpContext) 
            : base(context, httpContext)
        { }

        public async Task<List<T>> GetLookupsAsync<TKey, T>(params Expression<Func<T, object>>[] includes) where T : class, ILookup<TKey>
        {
            IQueryable<T> query = ApiDbContextExtensions.Set<T>(_context, includes);

            var items = await query.ToListAsync();

            return items;
        }

    }
}
