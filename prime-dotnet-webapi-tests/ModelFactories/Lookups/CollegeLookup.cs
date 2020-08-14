using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class CollegeLookup
    {
        private static IEnumerable<College> _seedData = new CollegeConfiguration().SeedData;

        public static IEnumerable<College> All { get { return _seedData; } }
    }
}
