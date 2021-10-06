using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CollegeLicenseLookup
    {
        private static IEnumerable<CollegeLicense> _seedData = new CollegeLicenseConfiguration().SeedData;

        public static IEnumerable<CollegeLicense> All { get { return _seedData; } }
    }
}
