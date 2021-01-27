using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models.Documents;

namespace Prime.Services
{
    public interface IEmailRenderingService
    {
        Task<IEnumerable<Pdf>> GenerateSiteRegistrationAttachmentsAsync(int siteId);
    }
}
