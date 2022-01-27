using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CollegePracticeLookup
    {
        private static IEnumerable<CollegePractice> _seedData = new CollegePracticeConfiguration().SeedData;

        public static IEnumerable<CollegePractice> All { get { return _seedData; } }
    }
}
