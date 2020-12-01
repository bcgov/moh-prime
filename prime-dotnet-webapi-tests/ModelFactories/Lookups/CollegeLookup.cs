using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTests.ModelFactories
{
    public static class CollegeLookup
    {
        private static IEnumerable<College> _seedData = new CollegeConfiguration().SeedData;

        public static IEnumerable<College> All { get { return _seedData; } }

        public static College PhysiciansAndSurgeons { get { return _seedData.Single(c => c.Prefix == "91"); } }
        public static College Pharmacists { get { return _seedData.Single(c => c.Prefix == "P1"); } }
        public static College NursesAndMidwives { get { return _seedData.Single(c => c.Prefix == "96"); } }
        public static IEnumerable<College> BigThree { get { return new[] { PhysiciansAndSurgeons, Pharmacists, NursesAndMidwives }; } }
    }
}
