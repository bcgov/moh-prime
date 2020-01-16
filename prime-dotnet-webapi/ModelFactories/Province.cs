using System.Linq;
using System.Collections.Generic;
using Bogus;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public class ProvinceFactory : Faker<Province>
    {
        private static ICollection<Province> SeedData { get { return new ProvinceConfiguration().SeedData; } }

        public ProvinceFactory()
        {
            StrictMode(true);

            Province province = null;
            RuleSet("BC", (set) => province = SeedData.Single(p => p.Code == Province.BRITISH_COLUMBIA_CODE));
            RuleSet("NotBC", (set) => province = SeedData.Skip(6).First());

            RuleFor(x => x.Code, () => province.Code);
            RuleFor(x => x.CountryCode, () => province.CountryCode);
            RuleFor(x => x.Country, () => province.Country);
            RuleFor(x => x.Name, () => province.Name);
        }
    }
}



