using System.Collections.Generic;
using Prime.Configuration;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class FacilityLookup
    {
        private static IEnumerable<Facility> _seedData = new FacilityConfiguration().SeedData;

        public static IEnumerable<Facility> All  { get { return _seedData; }  }
    }
}
