using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class GlobalClauseLookup
    {
        private static ICollection<GlobalClause> _seedData = new GlobalClauseConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<GlobalClause> All { get { return _seedData; } }
    }
}
