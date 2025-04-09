using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ISiteSubmissionService
    {
        Task CreateCommunitySiteSubmissionAsync(int siteId);
        Task CreateHealthAuthoritySiteSubmissionAsync(int siteId);
        Task<List<SiteSubmission>> GetSiteSubmissionsAsync(int siteId);
        Task<SiteSubmission> GetSiteSubmissionAsync(int siteId, int siteSubmissionId);
    }
}
