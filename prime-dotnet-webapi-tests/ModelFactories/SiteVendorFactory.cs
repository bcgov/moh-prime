using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class SiteVendorFactory : Faker<SiteVendor>
    {
        private static int IdCounter = 1;

        public SiteVendorFactory(Site site)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Site, f => site);
            RuleFor(x => x.VendorCode,
                f => f.PickRandom(VendorLookup.AllowedFor(
                    site.CareSettingCode ?? default(int))).Code);

            Ignore(x => x.SiteId);
            Ignore(x => x.Vendor);
        }
    }
}
