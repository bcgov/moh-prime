using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class PrivilegeGroupLookup
    {
        private static ICollection<PrivilegeGroup> _seedData = new PrivilegeGroupConfiguration().SeedData;

        public static ICollection<PrivilegeGroup> All { get { return _seedData; } }
    }
}
