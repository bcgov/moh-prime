using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Prime.ModelFactories
{
    public static class CollegePracticeLookup
    {
        private static ICollection<CollegePractice> _seedData = new CollegePracticeConfiguration().SeedData.AsQueryable().AsNoTracking().ToList();

        public static ICollection<CollegePractice> All { get { return _seedData; } }
    }
}
