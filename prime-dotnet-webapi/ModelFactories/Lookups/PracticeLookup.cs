using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class PracticeLookup
    {
        private static ICollection<Practice> _seedData = new PracticeConfiguration().SeedData;

        public static ICollection<Practice> All { get { return _seedData; } }

        public static IEnumerable<Practice> AllowedFor(short collegeCode)
        {
            var practices = CollegePracticeLookup.All
                .Where(cp => cp.CollegeCode == collegeCode)
                .Select(cp => All.Single(p => p.Code == cp.PracticeCode));

            // Practice is always optional, so add the option of null
            return practices.Append(null);
        }
    }
}
