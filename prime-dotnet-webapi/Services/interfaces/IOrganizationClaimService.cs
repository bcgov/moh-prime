
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IOrganizationClaimService
    {
        Task<bool> HasClaimAsync(int organizationId);

        /// <summary>
        /// Returns <c>true</c> if can successfully delete claim, <c>false</c> otherwise.
        /// </summary>
        Task<bool> DeleteClaimAsync(int claimId);

        Task<OrganizationClaim> GetOrganizationClaimAsync(int orgClaimId);
        Task<OrganizationClaim> GetOrganizationClaimByOrgIdAsync(int organizationId);
        Task<int> CreateOrganizationClaimAsync(OrganizationClaimViewModel claimOrganization, Organization organization);
        Task<bool> OrganizationClaimExistsAsync(OrganizationClaimSearchOptions search);
    }
}
