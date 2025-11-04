using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using System.Security.Claims;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ICommunitySiteService
    {
        Task<IEnumerable<CommunitySite>> GetSitesAsync(int? organizationId = null);
        Task<PaginatedList<CommunitySiteAdminListViewModel>> GetSitesAsync(OrganizationSearchOptions searchOptions);
        Task<CommunitySite> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(int organizationId);
        Task UpdateSiteAsync(int siteId, CommunitySiteUpdateModel updatedSite);
        Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid);
        Task<BusinessLicence> UpdateBusinessLicenceAsync(int businessLicenceId, BusinessLicence updateBusinessLicence);
        Task<IEnumerable<BusinessLicence>> GetBusinessLicencesAsync(int siteId);
        Task<BusinessLicence> GetLatestBusinessLicenceAsync(int siteId);
        Task<BusinessLicenceDocument> AddOrReplaceBusinessLicenceDocumentAsync(int businessLicenceId, Guid documentGuid);
        Task DeleteBusinessLicenceDocumentAsync(int businessLicenceId);
        Task<bool> SiteExistsAsync(int siteId);
        Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId);
        Task<IEnumerable<IndividualDeviceProviderViewModel>> GetIndividualDeviceProvidersAsync(int siteId);
        Task UpdateSigningAuthorityForOrganization(int organizationId, int partyId);
        Task<CommunitySiteViewModel> GetPredecessorSite(int siteId);
        Task<CommunitySiteViewModel> GetSuccessorSite(int siteId);
    }
}
