using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public sealed class EnrolleeHealthAuthorityFactory : Faker<EnrolleeHealthAuthority>
    {
        private static int _idCounter = 1;

        public EnrolleeHealthAuthorityFactory(Enrollee owner)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, f => _idCounter++);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.HealthAuthorityCode, f => f.PickRandom(HealthAuthorityLookup.All).Code);

            Ignore(x => x.HealthAuthority);
        }
    }
}
