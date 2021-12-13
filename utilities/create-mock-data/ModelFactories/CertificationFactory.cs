using System.Linq;
using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class CertificationFactory : Faker<Certification>
    {
        private static int IdCounter = 1;

        public CertificationFactory(Enrollee owner)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.LicenseNumber, f => f.Random.AlphaNumeric(5));
            RuleFor(x => x.PractitionerId, f => f.Random.Number(10000, 99999).ToString());
            RuleFor(x => x.RenewalDate, f => f.Date.Future());
            RuleFor(x => x.College, f => f.PickRandom(CollegeLookup.BigThree));
            RuleFor(x => x.CollegeCode, (f, x) => x.College.Code);
            RuleFor(x => x.LicenseCode, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode)).Code);
            RuleFor(x => x.PracticeCode, (f, x) => f.PickRandom(PracticeLookup.AllowedFor(x.CollegeCode))?.Code);

            Ignore(x => x.License);
            Ignore(x => x.Practice);

            RuleSet("licence.manual", (set) =>
            {
                RuleFor(x => x.LicenseCode, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => l.Manual)).Code);
            });
            RuleSet("licence.auto", (set) =>
            {
                RuleFor(x => x.LicenseCode, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => !l.Manual)).Code);
            });
            RuleSet("licence.regulated", (set) =>
            {
                RuleFor(x => x.LicenseCode, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => l.NamedInImReg)).Code);
            });
            RuleSet("licence.nonRegulated", (set) =>
            {
                RuleFor(x => x.LicenseCode, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => !l.NamedInImReg)).Code);
            });

            FinishWith((f, x) =>
            {
                // Clear lookup navigation properties
                x.College = null;
            });
        }
    }
}
