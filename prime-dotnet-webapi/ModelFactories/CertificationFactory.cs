using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class CertificationFactory : Faker<Certification>
    {
        private static int IdCounter = 1;

        public CertificationFactory(Enrollee owner)
        {
            StrictMode(true);
            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.College, f => f.PickRandom(CollegeLookup.All));
            RuleFor(x => x.CollegeCode, (f, x) => x.College.Code);
            RuleFor(x => x.LicenseNumber, () => null);
            RuleFor(x => x.License, f => f.PickRandom(LicenseLookup.All));
            RuleFor(x => x.LicenseCode, (f, x) => x.License.Code);
            RuleFor(x => x.RenewalDate, f => f.Date.Future());
            RuleFor(x => x.Practice, f => f.PickRandom(PracticeLookup.All));
            RuleFor(x => x.PracticeCode, (f, x) => x.Practice.Code);
        }
    }
}
