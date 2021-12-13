using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CountryLookup
    {
        private static IEnumerable<Country> _seedData = new CountryConfiguration().SeedData;

        public static IEnumerable<Country> All { get { return _seedData; } }

        public static Country Canada { get { return All.Single(c => c.Code == Country.CANADA); } }

        public static Country ByCode(string countryCode)
        {
            return All.SingleOrDefault(c => c.Code == countryCode);
        }
    }
}
