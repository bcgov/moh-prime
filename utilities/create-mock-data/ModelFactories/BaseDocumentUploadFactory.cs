using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class BusinessLicenceDocumentFactory : BaseDocumentUploadFactory<BusinessLicenceDocument>
    {
        public BusinessLicenceDocumentFactory(BusinessLicence related)
        {
//            this.SetBaseRules();

            RuleFor(x => x.BusinessLicence, f => related);

            Ignore(x => x.BusinessLicenceId);
        }
    }


    public abstract class BaseDocumentUploadFactory<T> : Faker<T> where T : BaseDocumentUpload
    {
        protected BaseDocumentUploadFactory()
        {
//            this.SetBaseRules();

            RuleFor(x => x.Id, f => IdCounter.Id++);
            RuleFor(x => x.DocumentGuid, f => f.Random.Guid());
            RuleFor(x => x.Filename, f => f.Random.Word());
            // TODO: add additional logic?
            RuleFor(x => x.UploadedDate, f => f.Date.Past(1));
        }
    }
}