using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISiteRegistrationService
    {
        bool CanPerformSiteStatusAction(SiteStatusAction action, SiteStatusType currentStatus);
    }
}
