using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class OboSiteFactory : Faker<OboSite>
    {
        private static int IdCounter = 1;

        public OboSiteFactory(Enrollee owner)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Enrollee, () => owner);
            RuleFor(x => x.EnrolleeId, () => owner.Id);
            RuleFor(x => x.CareSettingCode, f => f.PickRandom(CareSettingLookup.All).Code);
            RuleFor(x => x.HealthAuthorityCode, f => f.PickRandom(HealthAuthorityLookup.All).Code);
            RuleFor(x => x.SiteName, f => f.Lorem.Word());
            RuleFor(x => x.PEC, f => f.Address.BuildingNumber());
            RuleFor(x => x.FacilityName, f => f.Lorem.Word());
            RuleFor(x => x.JobTitle, f => f.Lorem.Word());
            RuleFor(x => x.PhysicalAddress, f => new PhysicalAddressFactory().Generate());

            Ignore(x => x.CareSetting);
            Ignore(x => x.HealthAuthority);

            FinishWith((f, x) =>
                {
                    if (x.CareSettingCode != (int)CareSettingType.HealthAuthority)
                    {
                        // HealthAuthorityCode should only be populated if CareSettingCode is appropriate
                        x.HealthAuthorityCode = null;
                    }
                });
        }
    }
}
