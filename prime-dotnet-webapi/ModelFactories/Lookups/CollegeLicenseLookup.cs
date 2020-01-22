using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class CollegeLicenseLookup
    {
        private static ICollection<CollegeLicense> _seedData = new CollegeLicenseConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<CollegeLicense> All { get { return _seedData; } }
    }
}
