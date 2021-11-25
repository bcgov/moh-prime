using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class OrganizationFactory : Faker<Organization>
    {
        private static int IdCounter = 1;

        public OrganizationFactory(Party signingAuthority)
        {
            // this.SetBaseRules();  ...

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Name, f => f.Company.CompanyName());

            RuleFor(x => x.SigningAuthority, () => signingAuthority);

            // ... Incomplete
        }
    }
}
