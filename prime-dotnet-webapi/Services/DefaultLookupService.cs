using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<T>> GetLookupsAsync<T>() where T : ILookup
        {
            IQueryable<T> query = ApiDbContextExtensions.Set<T>(_context);

            var items = await query.ToListAsync();

            return items;
        }

    }
}