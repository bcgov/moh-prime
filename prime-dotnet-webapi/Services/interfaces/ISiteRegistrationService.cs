using System.Threading.Tasks;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISiteRegistrationService
    {
        bool CanPerformSiteStatusAction(SiteStatusAction action);
    }
}
