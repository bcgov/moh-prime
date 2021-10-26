using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Sites;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<bool> PecAssignableAsync(int siteId, string pec);
        Task UpdateCompletedAsync(int siteId, bool completed);
        Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> ApproveSite(int siteId);
        Task<Site> DeclineSite(int siteId);
        Task<Site> UnrejectSite(int siteId);
        Task<Site> EnableEditingSite(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<IEnumerable<BusinessDayViewModel>> GetBusinessHoursAsync(int siteId);
        Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsersAsync(int siteId);
        Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(int siteId);
        Task<SiteRegistrationNoteViewModel> GetSiteRegistrationNoteAsync(int siteId, int siteRegistrationNoteId);
        Task<IEnumerable<RemoteAccessSearchViewModel>> GetRemoteUserInfoAsync(IEnumerable<CertSearchViewModel> certs);
    }
}
