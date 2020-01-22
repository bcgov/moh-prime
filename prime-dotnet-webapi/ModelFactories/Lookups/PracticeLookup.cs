using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class PracticeLookup
    {
        private static ICollection<Practice> _seedData = new PracticeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<Practice> All { get { return _seedData; } }

        public static IEnumerable<Practice> AllowedFor(College college)
        {
            var practices = CollegePracticeLookup.All
                .Where(cp => cp.CollegeCode == college.Code)
                .Select(cp => _seedData.Single(p => p.Code == cp.PracticeCode));

            // Practice is always optional, so add the option of null
            return practices.Append(null);
        }
    }
}
