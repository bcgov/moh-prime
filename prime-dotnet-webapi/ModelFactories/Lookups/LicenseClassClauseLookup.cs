using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class LicenseClassClauseLookup
    {
        private static ICollection<LicenseClassClause> _seedData = new LicenseClassClauseConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<LicenseClassClause> All { get { return _seedData; } }
    }
}
