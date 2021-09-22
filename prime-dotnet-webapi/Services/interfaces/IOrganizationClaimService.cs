
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IOrganizationClaimService
    {
        Task<bool> HasClaimAsync(int organizationId);
        Task DeleteClaimAsync(int claimId);
        Task<OrganizationClaim> GetOrganizationClaimAsync(int orgClaimId);
        Task<OrganizationClaim> GetOrganizationClaimByOrgIdAsync(int organizationId);
        Task<int> CreateOrganizationClaimAsync(OrganizationClaimViewModel claimOrganization, Organization organization);
        Task<bool> OrganizationClaimExistsAsync(OrganizationClaimSearchOptions search);
    }
}
