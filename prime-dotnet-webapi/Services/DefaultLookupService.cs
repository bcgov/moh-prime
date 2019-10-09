using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultLookupService : ILookupService
    {
        private readonly ApiDbContext _context;

        public DefaultLookupService(
            ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetLookupsAsync<T>(params Expression<Func<T, object>>[] includes) where T : class, ILookup
        {
            IQueryable<T> query = ApiDbContextExtensions.Set<T>(_context, includes);

            var items = await query.ToListAsync();

            return items;
        }

    }
}