using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class LicenseClassClauseLookup
    {
        private static ICollection<LicenseClassClause> _seedData = new LicenseClassClauseConfiguration().SeedData;

        public static ICollection<LicenseClassClause> All { get { return _seedData; } }
    }
}
