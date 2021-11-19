using Bogus;
using Bogus.Extensions;

using Prime.Models;


namespace PrimeTests.ModelFactories
{
    public class ContactFactory : Faker<Contact>
    {
        private static int IdCounter = 1;

        public ContactFactory()
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.FirstName, f => f.Name.FirstName());
            RuleFor(x => x.LastName, f => f.Name.LastName());
            RuleFor(x => x.JobRoleTitle, f => f.Name.JobTitle());
            RuleFor(x => x.Email, f => f.Internet.Email());
            RuleFor(x => x.Phone, f => f.Phone.PhoneNumber());
            RuleFor(x => x.Fax, f => f.Phone.PhoneNumber().OrNull(f, 0.70f));
            RuleFor(x => x.SMSPhone, f => f.Phone.PhoneNumber().OrNull(f));
            RuleFor(x => x.PhysicalAddress, f => new PhysicalAddressFactory().Generate());
        }
    }
}
