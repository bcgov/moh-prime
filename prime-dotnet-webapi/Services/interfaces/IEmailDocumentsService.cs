using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models.Documents;

namespace Prime.Services.EmailInternal
{
    public interface IEmailDocumentsService
    {
        Task<string> GetBusinessLicenceDownloadLink(int businessLicenceId);
        Task<IEnumerable<Pdf>> GenerateSiteRegistrationSubmissionAttachmentsAsync(int siteId);
        Task SaveSiteRegistrationReview(int siteId, Pdf pdf);
    }
}
