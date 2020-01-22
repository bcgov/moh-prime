using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class LicenseLookup
    {
        private static ICollection<License> _seedData = new LicenseConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<License> All { get { return _seedData; } }

        public static IEnumerable<License> AllowedFor(College college)
        {
            return CollegeLicenseLookup.All
                .Where(cl => cl.CollegeCode == college.Code)
                .Select(cl => _seedData.Single(l => l.Code == cl.LicenseCode));
        }
    }
}
