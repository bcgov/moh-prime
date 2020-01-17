using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class ProvinceLookup
    {
        private static ICollection<Province> _seedData = new ProvinceConfiguration().SeedData;

        public static Province BC
        {
            get { return _seedData.Single(p => p.Code == Province.BRITISH_COLUMBIA_CODE); }
        }
        public static IEnumerable<Province> NotBC
        {
            get { return _seedData.Where(p => p.Code != Province.BRITISH_COLUMBIA_CODE); }
        }
    }
}
