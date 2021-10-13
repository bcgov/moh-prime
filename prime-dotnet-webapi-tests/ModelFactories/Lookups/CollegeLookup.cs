using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class CollegeLookup
    {
        private static IEnumerable<College> _seedData = new CollegeConfiguration().SeedData;

        public static IEnumerable<College> All { get { return _seedData; } }

        public static College PhysiciansAndSurgeons { get => _seedData.Single(c => c.Code == 1); }
        public static College Pharmacists { get => _seedData.Single(c => c.Code == 2); }
        public static College NursesAndMidwives { get => _seedData.Single(c => c.Code == 3); }
        public static IEnumerable<College> BigThree { get => new[] { PhysiciansAndSurgeons, Pharmacists, NursesAndMidwives }; }
    }
}
