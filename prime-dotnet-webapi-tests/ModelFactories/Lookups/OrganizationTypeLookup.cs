using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTest.ModelFactories
{
    public static class OrganizationTypeLookup
    {
        private static ICollection<OrganizationType> _seedData = new OrganizationTypeConfiguration().SeedData;

        public static ICollection<OrganizationType> All { get { return _seedData; } }
    }
}
