using System.Collections.Generic;
using Prime.Configuration.Database;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class HealthAuthorityLookup
    {
        private static IEnumerable<HealthAuthority> _seedData = new HealthAuthorityConfiguration().SeedData;

        public static IEnumerable<HealthAuthority> All { get { return _seedData; } }
    }
}
