using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class PhysicalAddressFactory : Faker<PhysicalAddress>
    {
        private static int IdCounter = 1;
        public PhysicalAddressFactory(Enrollee owner)
        {
            StrictMode(true);
            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.Country, (f, x) => x.Province.Country);
            RuleFor(x => x.CountryCode, (f, x) => x.Country.Code);
            RuleFor(x => x.Province, () => ProvinceLookup.BC);
            RuleSet("notBC", (set) =>
            {
                set.RuleFor(x => x.Province, f => f.PickRandom(ProvinceLookup.NotBC));
            });
            RuleFor(x => x.ProvinceCode, (f, x) => x.Province.Code);
            RuleFor(x => x.Street, f => f.Address.StreetAddress());
            RuleFor(x => x.Street2, f => f.Address.StreetAddress());
            RuleFor(x => x.City, f => f.Address.City());
            RuleFor(x => x.Postal, f => f.Address.ZipCode("?#?#?#"));
        }
    }
}
