using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class JobNameLookup
    {
        private static ICollection<JobName> _seedData = new JobNameConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<JobName> All { get { return _seedData; } }
    }
}
