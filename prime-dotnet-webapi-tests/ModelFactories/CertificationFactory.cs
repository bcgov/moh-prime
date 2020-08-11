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
            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.LicenseNumber, f => f.Random.AlphaNumeric(5));
            RuleFor(x => x.RenewalDate, f => f.Date.Future());
            RuleFor(x => x.College, f => f.PickRandom(CollegeLookup.All));
            RuleFor(x => x.CollegeCode, (f, x) => x.College.Code);
            RuleFor(x => x.License, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode)));
            RuleFor(x => x.Practice, (f, x) => f.PickRandom(PracticeLookup.AllowedFor(x.CollegeCode)));
            RuleFor(x => x.PracticeCode, (f, x) => x.Practice?.Code);

            // Finish With
            Ignore(x => x.LicenseCode);

            RuleSet("licence.manual", (set) =>
            {
                RuleFor(x => x.License, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => l.Manual)));
            });
            RuleSet("licence.auto", (set) =>
            {
                RuleFor(x => x.License, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => !l.Manual)));
            });
            RuleSet("licence.regulated", (set) =>
            {
                RuleFor(x => x.License, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => l.RegulatedUser)));
            });
            RuleSet("licence.nonRegulated", (set) =>
            {
                RuleFor(x => x.License, (f, x) => f.PickRandom(LicenseLookup.AllowedFor(x.CollegeCode).Where(l => !l.RegulatedUser)));
            });

            FinishWith((f, x) =>
            {
                RuleFor(x => x.LicenseCode, (f, x) => x.License.Code);
            });
        }
    }
}
