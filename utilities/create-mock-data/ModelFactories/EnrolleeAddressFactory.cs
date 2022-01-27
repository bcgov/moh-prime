using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class EnrolleeAddressFactory : Faker<EnrolleeAddress>
    {
        private static int IdCounter = 1;

        public EnrolleeAddressFactory(Enrollee owner)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Address, f => new VerifiedAddressFactory().Generate());
            RuleFor(x => x.AddressId, (f, x) => x.Address.Id);

            RuleSet("physical", (set) =>
            {
                RuleFor(x => x.Address, f => new PhysicalAddressFactory().Generate());
                RuleFor(x => x.AddressId, (f, x) => x.Address.Id);
            });

            RuleSet("mailing", (set) =>
            {
                RuleFor(x => x.Address, f => new MailingAddressFactory().Generate());
                RuleFor(x => x.AddressId, (f, x) => x.Address.Id);
            });
        }
    }
}
