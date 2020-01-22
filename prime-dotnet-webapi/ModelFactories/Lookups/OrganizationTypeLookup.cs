using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class OrganizationTypeLookup
    {
        private static ICollection<OrganizationType> _seedData = new OrganizationTypeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<OrganizationType> All { get { return _seedData; } }
    }
}
