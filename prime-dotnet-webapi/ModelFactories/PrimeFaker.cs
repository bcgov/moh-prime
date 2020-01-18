using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class PhysicalAddressFactory : Faker<PhysicalAddress>
    {
        private static int IdCounter = 1;
        public PhysicalAddressFactory(Enrollee owner)
        {
            StrictMode(true);
            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Province, f => ProvinceLookup.BC);
            RuleFor(x => x.ProvinceCode, (f, x) => x.Province.Code);
            RuleFor(x => x.Country, f => CountryLookup.Canada);
            RuleFor(x => x.CountryCode, (f, x) => x.Country.Code);
            RuleFor(x => x.Street, f => f.Address.StreetAddress());
            RuleFor(x => x.Street2, f => f.Address.StreetAddress().OrNull(f));
            RuleFor(x => x.City, f => f.Address.City());
            RuleFor(x => x.Postal, f => f.Address.ZipCode("?#?#?#"));

            RuleSet("notBC", (set) =>
            {
                set.RuleFor(x => x.Province, f => f.PickRandom(ProvinceLookup.NotBC));
                set.RuleFor(x => x.ProvinceCode, (f, x) => x.Province.Code);
                set.RuleFor(x => x.Country, (f, x) => CountryLookup.ByCode(x.Province.CountryCode));
                set.RuleFor(x => x.CountryCode, (f, x) => x.Country.Code);
            });
        }
    }
}
