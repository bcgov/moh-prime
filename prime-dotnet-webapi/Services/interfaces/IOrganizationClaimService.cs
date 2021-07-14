
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IOrganizationClaimService
    {
        Task<bool> HasClaimAsync(int organizationId);

        /// <summary>
        /// Returns <c>true</c> if can successfully delete claim, <c>false</c> otherwise.
        /// </summary>
        Task<bool> DeleteClaimAsync(int claimId);

        Task<OrganizationClaim> GetOrganizationClaimAsync(int organizationId);
    }
}
