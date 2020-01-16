using Bogus;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public class PhysicalAddressFactory : Faker<Address>
    {
        private static int IdCounter = 1;

        public PhysicalAddressFactory(Enrollee owner)
        {
            StrictMode(true);
            RuleFor(x => x.Id, () => IdCounter++);
                         RuleFor(x => x.EnrolleeId, () => owner.Id);
                   RuleFor(x => x.Enrollee, () => owner );
            RuleFor(x => x.CountryCode, f => f.PickRandom(countries));
                        RuleFor(x => x.Country, f => f.PickRandom(CountryConfiguration));
            RuleFor(x => x.ProvinceCode, TestUtils.RandomProvinceCode());
            RuleFor(x => x.Street, f => f.Address.StreetAddress());
            RuleFor(x => x.City, f => f.Address.City());
            RuleFor(x => x.Postal, f => f.Address.ZipCode("?#?#?#"));






        }
    }
}
