using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class EnrolleeCareSettingFactory : Faker<EnrolleeCareSetting>
    {
        private static int IdCounter = 1;

        public EnrolleeCareSettingFactory(Enrollee owner)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.CareSettingCode, f => f.PickRandom(CareSettingLookup.All).Code);

            Ignore(x => x.CareSetting);
        }
    }
}
