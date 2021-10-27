using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class SiteVendorFactory : Faker<SiteVendor>
    {
        private static int IdCounter = 1;

        public SiteVendorFactory(Site site)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, () => IdCounter++);
            RuleFor(x => x.Site, f => site);
            RuleFor(x => x.Vendor,
                (f, x) => f.PickRandom(VendorLookup.AllowedFor(
                    x.Site.CareSetting.Code)));

            Ignore(x => x.SiteId);
            Ignore(x => x.VendorCode);
        }
    }
}
