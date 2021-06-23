using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISiteRegistrationService
    {
        bool CanPerformSiteStatusAction(SiteRegistrationAction action, SiteStatusType currentStatus);
    }
}
