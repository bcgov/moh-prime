using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class VerifiedAddressFactory : AddressFactory<VerifiedAddress> { }
    public class PhysicalAddressFactory : AddressFactory<PhysicalAddress> { }
    public class MailingAddressFactory : AddressFactory<MailingAddress> { }

    public abstract class AddressFactory<T> : Faker<T> where T : Address
    {
        protected AddressFactory()
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, f => IdCounter.Id++);
            RuleFor(x => x.Province, f => null);
            RuleFor(x => x.ProvinceCode, f => ProvinceLookup.BC.Code);
            RuleFor(x => x.CountryCode, f => CountryLookup.Canada.Code);
            RuleFor(x => x.Street, f => f.Address.StreetAddress());
            RuleFor(x => x.Street2, f => f.Address.StreetAddress().OrNull(f));
            RuleFor(x => x.City, f => f.Address.City());
            RuleFor(x => x.Postal, f => f.Address.ZipCode("?#?#?#"));

            Ignore(x => x.Country);

            RuleSet("notBC", (set) =>
            {
                set.RuleFor(x => x.Province, f => f.PickRandom(ProvinceLookup.NotBC));
                set.RuleFor(x => x.ProvinceCode, (f, x) => x.Province.Code);
                set.RuleFor(x => x.CountryCode, (f, x) => CountryLookup.ByCode(x.Province.CountryCode).Code);
                set.RuleFor(x => x.Postal, (f, x) => f.Address.ZipCode(x.CountryCode == Country.CANADA ? "?#?#?#" : null));
                set.FinishWith((f, x) =>
                {
                    // Clear lookup table navigation properties
                    x.Province = null;
                });
            });
        }
    }

    internal class IdCounter
    {
        public static int Id = 1;
    }
}
