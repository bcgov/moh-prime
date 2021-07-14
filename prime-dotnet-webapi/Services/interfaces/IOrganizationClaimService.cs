
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

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

        Task<Organization> ClaimOrganizationAsync(OrganizationClaimViewModel organizationClaim, Organization organization);

        Task<bool> OrganizationClaimExistsAsync(OrganizationClaimSearchOptions search);
    }
}
