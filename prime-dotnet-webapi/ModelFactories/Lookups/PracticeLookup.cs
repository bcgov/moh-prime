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

        public static IEnumerable<Practice> AllowedFor(College college)
        {
            return CollegePracticeLookup.All
               .Where(x => x.College.Code == college.Code)
               .Select(x => x.Practice);
        }
    }
}
