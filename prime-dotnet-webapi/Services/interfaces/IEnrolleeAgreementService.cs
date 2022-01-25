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
        /// <summary>
        /// Returns whether the enrollee's current agreement is identical to the agreement they would be
        /// assigned as of today.  Returns <c>true</c> if match, <c>false</c> if not identical
        /// or if cannot be automatically determined which agreement would be assigned
        /// </summary>
        Task<bool> IsAgreementTypeIdenticalAsync(int enrolleeId);
    }
}
