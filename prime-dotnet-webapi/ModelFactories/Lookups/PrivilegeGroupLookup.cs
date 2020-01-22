using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class PrivilegeGroupLookup
    {
        private static ICollection<PrivilegeGroup> _seedData = new PrivilegeGroupConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<PrivilegeGroup> All { get { return _seedData; } }
    }
}
