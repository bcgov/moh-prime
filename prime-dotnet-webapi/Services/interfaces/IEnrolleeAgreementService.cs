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
        /// Returns whether the enrollee's current agreement type is OBO and changing to RU if their agreement would be
        /// assigned as of today.  Returns <c>true</c> if such a change, <c>false</c> if not such a change OR
        /// if cannot be automatically determined which agreement would be assigned OR enrollee does yet have an agreement
        /// </summary>
        Task<bool> IsOboToRuAgreementTypeChangeAsync(int enrolleeId);
        Task<AgreementGroup?> GetCurrentAgreementGroupForAnEnrolleeAsync(int enrolleeId);
        /// <summary>
        /// An agreement that was never accepted can be deleted.
        /// </summary>
        /// <param name="enrolleeId"></param>
        Task DeleteObsoleteEnrolleeAgreementAsync(int enrolleeId);
    }
}
