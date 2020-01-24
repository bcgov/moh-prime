using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class PrivilegeTypeLookup
    {
        private static ICollection<PrivilegeType> _seedData = new PrivilegeTypeConfiguration().SeedData;

        public static ICollection<PrivilegeType> All { get { return _seedData; } }
    }
}
