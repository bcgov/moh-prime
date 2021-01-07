using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetSitesAsync(int? organizationId = null);
        Task<Site> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(int organizationId);
        Task<int> UpdateSiteAsync(int siteId, SiteUpdateModel updatedSite);
        Task<int> UpdateCompletedAsync(int siteId);
        Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> ApproveSite(int siteId);
        Task<Site> DeclineSite(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);

        Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid);
        Task<BusinessLicence> UpdateBusinessLicenceAsync(int siteId, BusinessLicence updateBusinessLicence);
        Task<BusinessLicence> GetBusinessLicenceAsync(int siteId);
        Task<BusinessLicenceDocument> AddOrReplaceBusinessLicenceDocumentAsync(int businessLicenceId, Guid documentGuid);
        Task DeleteBusinessLicenceDocumentAsync(int siteId);
        Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId);
        Task<IEnumerable<SiteAdjudicationDocument>> GetSiteAdjudicationDocumentsAsync(int siteId);
        Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId);
        Task<IEnumerable<Site>> GetSitesByRemoteUserInfoAsync(IEnumerable<Certification> certifications);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(Site site);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEventsAsync(int siteId, IEnumerable<int> businessEventTypeCodes);
        Task<SiteAdjudicationDocument> GetSiteAdjudicationDocumentAsync(int documentId);
        Task DeleteSiteAdjudicationDocumentAsync(int documentId);

    }
}
