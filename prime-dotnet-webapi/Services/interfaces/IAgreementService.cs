using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAgreementService
    {
        Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false);

        Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters);

        Task CreateEnrolleeAgreementAsync(int enrolleeId);

        Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task<IEnumerable<AgreementViewModel>> GetOrgAgreementsAsync(int organizationId);

        Task<Agreement> GetOrgAgreementAsync(int organizationId, int agreementId, bool asEncodedPdf = false);
    }
}
