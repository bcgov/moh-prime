using Prime.Models.Api;
using Prime.Engines;
using Prime.Models;

namespace Prime.Services
{
    public class SiteRegistrationService : ISiteRegistrationService
    {
        public bool CanPerformSiteStatusAction(SiteRegistrationAction action, SiteStatusType currentStatus)
        {
            SiteStatusType newStatus;

            switch (action)
            {
                case SiteRegistrationAction.Submit:
                case SiteRegistrationAction.Undecline:
                    newStatus = SiteStatusType.InReview;
                    break;
                case SiteRegistrationAction.Approve:
                    newStatus = SiteStatusType.Approved;
                    break;
                case SiteRegistrationAction.Decline:
                    newStatus = SiteStatusType.Locked;
                    break;
                case SiteRegistrationAction.RequestChange:
                case SiteRegistrationAction.Unapprove:
                    newStatus = SiteStatusType.Active;
                    break;
                default:
                    return false;
            }
            return SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);
        }
    }
}
