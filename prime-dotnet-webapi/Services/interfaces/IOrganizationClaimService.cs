
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IOrganizationClaimService
    {
        Task<bool> HasClaimAsync(int organizationId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="partyId"></param>
        /// <returns><c>true</c> if can successfully delete claim, <c>false</c> otherwise.</returns>
        Task<bool> DeleteClaimAsync(int organizationId, int partyId);

        Task<OrganizationClaim> GetOrganizationClaimAsync(int organizationId);
    }
}
