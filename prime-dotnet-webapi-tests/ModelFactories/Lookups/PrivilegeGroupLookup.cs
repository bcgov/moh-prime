using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class PrivilegeGroupLookup
    {
        private static IEnumerable<PrivilegeGroup> _seedData = new PrivilegeGroupConfiguration().SeedData;

        public static IEnumerable<PrivilegeGroup> All { get { return _seedData; } }
    }
}
