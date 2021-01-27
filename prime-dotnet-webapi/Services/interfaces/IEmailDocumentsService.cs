using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models.Documents;

namespace Prime.Services.EmailInternal
{
    public interface IEmailDocumentsService
    {
        Task<IEnumerable<Pdf>> GenerateSiteRegistrationAttachmentsAsync(int siteId);
    }
}
