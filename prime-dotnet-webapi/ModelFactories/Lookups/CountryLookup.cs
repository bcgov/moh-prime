using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class CountryLookup
    {
        private static ICollection<Country> _seedData = new CountryConfiguration().SeedData;

        public static ICollection<Country> All { get { return _seedData; } }
    }
}
