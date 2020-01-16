using System.Linq;
using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class ProvinceLookup
    {
        private static ICollection<Province> seedData = new ProvinceConfiguration().SeedData;

        public static Province BC
        {
            get
            {
                return seedData.Single(p => p.Code == Province.BRITISH_COLUMBIA_CODE);
            }
        }

        public static IEnumerable<Province> NotBC
        {
            get
            {
                return seedData.Where(p => p.Code != Province.BRITISH_COLUMBIA_CODE);
            }
        }
    }
}
