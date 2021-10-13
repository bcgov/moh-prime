using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Database;

namespace PrimeTests.ModelFactories
{
    public static class PracticeLookup
    {
        private static IEnumerable<Practice> _seedData = new PracticeConfiguration().SeedData;

        public static IEnumerable<Practice> All { get { return _seedData; } }

        public static IEnumerable<Practice> AllowedFor(int collegeCode)
        {
            var practices = CollegePracticeLookup.All
                .Where(cp => cp.CollegeCode == collegeCode)
                .Select(cp => All.Single(p => p.Code == cp.PracticeCode));

            // Practice is always optional, so add the option of null
            return practices.Append(null);
        }
    }
}
