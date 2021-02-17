using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class SiteFactory : Faker<Site>
    {
        private static int IdCounter = 1;

        public SiteFactory(Organization org = null)
        {
            // this.SetBaseRules();  ...

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.PhysicalAddress, f => new PhysicalAddressFactory().Generate());
            RuleFor(x => x.Organization, f => org != null ? org : new OrganizationFactory().Generate());
            RuleFor(x => x.OrganizationId, (f, x) => x.Organization.Id);

            Ignore(s => s.AdministratorPharmaNetId);

            // ... Incomplete
        }
    }
}
