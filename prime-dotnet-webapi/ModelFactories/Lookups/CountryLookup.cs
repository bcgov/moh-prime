using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class CountryLookup
    {
        private static ICollection<Country> _seedData = new CountryConfiguration().SeedData;

        public static ICollection<Country> All { get { return _seedData; } }
        public static Country Canada { get { return _seedData.Single(c => c.Code == "CA"); } }

        public static Country ByCode(string countryCode)
        {
            return _seedData.SingleOrDefault(c => c.Code == countryCode);
        }
    }
}
