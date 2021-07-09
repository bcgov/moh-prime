using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IEnrolleeAgreementService
    {
        Task<Agreement> GetCurrentAgreementAsync(int enrolleeId);
        Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false);
        Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters);
        Task CreateEnrolleeAgreementAsync(int enrolleeId);
        Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId);
        Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId);
    }
}
