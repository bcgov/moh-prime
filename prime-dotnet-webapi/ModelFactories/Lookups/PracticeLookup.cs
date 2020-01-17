using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class PracticeLookup
    {
        private static ICollection<Practice> _seedData = new PracticeConfiguration().SeedData;

        public static ICollection<Practice> All { get { return _seedData; } }
    }
}
