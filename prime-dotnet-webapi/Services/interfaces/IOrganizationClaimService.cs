
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IOrganizationClaimService
    {
        Task<bool> IsOrganizationUnderReviewAsync(int organizationId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="partyId"></param>
        /// <returns><c>true</c> if can successfully delete claim, <c>false</c> otherwise.</returns>
        Task<bool> DeleteClaimAsync(int organizationId, int partyId);
    }
}
