using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class PrivilegeTypeLookup
    {
        private static ICollection<PrivilegeType> _seedData = new PrivilegeTypeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<PrivilegeType> All { get { return _seedData; } }
    }
}
