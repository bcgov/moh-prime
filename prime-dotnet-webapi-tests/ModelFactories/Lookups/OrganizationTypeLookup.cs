using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class OrganizationTypeLookup
    {
        private static IEnumerable<OrganizationType> _seedData = new OrganizationTypeConfiguration().SeedData;

        public static IEnumerable<OrganizationType> All { get { return _seedData; } }
    }
}
