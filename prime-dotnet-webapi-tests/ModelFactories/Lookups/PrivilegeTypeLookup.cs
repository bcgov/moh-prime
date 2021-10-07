using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class PrivilegeTypeLookup
    {
        private static IEnumerable<PrivilegeType> _seedData = new PrivilegeTypeConfiguration().SeedData;

        public static IEnumerable<PrivilegeType> All { get { return _seedData; } }
    }
}
