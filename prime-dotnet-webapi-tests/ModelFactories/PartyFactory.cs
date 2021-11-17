using Bogus;
using Bogus.Extensions;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class PartyFactory : Faker<Party>
    {
        private static int IdCounter = 1;

        public PartyFactory()
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.UserId, f => f.Random.Guid());
            RuleFor(x => x.HPDID, f => f.Random.String(40));
            RuleFor(x => x.FirstName, f => f.Name.FirstName());
            RuleFor(x => x.LastName, f => f.Name.LastName());
            RuleFor(x => x.DateOfBirth, f => f.Person.DateOfBirth);

            RuleFor(x => x.JobRoleTitle, f => f.Name.JobTitle());
            RuleFor(x => x.Email, f => f.Internet.Email());
            RuleFor(x => x.Phone, f => f.Phone.PhoneNumber());
            RuleFor(x => x.Fax, f => f.Phone.PhoneNumber().OrNull(f, 0.70f));
            RuleFor(x => x.SMSPhone, f => f.Phone.PhoneNumber().OrNull(f));

            Ignore(x => x.GivenNames);
            Ignore(x => x.PreferredFirstName);
            Ignore(x => x.PreferredMiddleName);
            Ignore(x => x.PreferredLastName);
            Ignore(x => x.PhoneExtension);
            // TODO:
            Ignore(x => x.Addresses);
            // TODO:
            Ignore(x => x.Agreements);
            Ignore(x => x.PartyEnrolments);
            // TODO:
            Ignore(x => x.PartySubmissions);
            Ignore(x => x.PartyCertifications);


        }
    }
}