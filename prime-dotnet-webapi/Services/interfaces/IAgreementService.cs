using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IAgreementService
    {
        Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false);

        Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters);

        Task CreateEnrolleeAgreementAsync(int enrolleeId);

        Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task<string> GetOrgAgreementTextAsync(int organizationId, int agreementId, bool asEncodedPdf = false);
    }
}
