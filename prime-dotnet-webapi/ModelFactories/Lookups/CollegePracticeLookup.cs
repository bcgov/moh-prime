using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class CollegePracticeLookup
    {
        private static ICollection<CollegePractice> _seedData = new CollegePracticeConfiguration().SeedData;

        public static ICollection<CollegePractice> All { get { return _seedData; } }
    }
}
