using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class BusinessLicenceFactory : Faker<BusinessLicence>
    {
        private static int IdCounter = 1;

        public BusinessLicenceFactory(Site owner)
        {
//            this.SetBaseRules();

//            RuleFor(x => x.Id, f => IdCounter++);
            RuleFor(x => x.Site, f => owner);
            // TODO: What dates are generated?  Have logic related to Site SubmittedDate?
            RuleFor(x => x.UploadedDate, f => f.Date.Past(1));
            // TODO: What dates are generated?  Have logic related to Site SubmittedDate?
            RuleFor(x => x.ExpiryDate, f => f.Date.Future(2));
            RuleFor(x => x.BusinessLicenceDocument, (f, x) => new BusinessLicenceDocumentFactory(x).Generate());

            Ignore(x => x.DeferredLicenceReason);
            Ignore(x => x.SiteId);
        }
    }
}