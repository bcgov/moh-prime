using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class UserClauseLookup
    {
        private static ICollection<UserClause> _seedData = new UserClauseConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<UserClause> All { get { return _seedData; } }
    }
}
