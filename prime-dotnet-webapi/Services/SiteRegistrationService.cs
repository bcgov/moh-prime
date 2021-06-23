using Prime.Models.Api;
using Prime.Engines;
using Prime.Models;

namespace Prime.Services
{
    public class SiteRegistrationService : ISiteRegistrationService
    {
        public bool CanPerformSiteStatusAction(SiteStatusAction action, SiteStatusType currentStatus)
        {
            SiteStatusType newStatus;

            switch (action)
            {
                case SiteStatusAction.Submit:
                case SiteStatusAction.Undecline:
                    newStatus = SiteStatusType.InReview;
                    break;
                case SiteStatusAction.Approve:
                    newStatus = SiteStatusType.Approved;
                    break;
                case SiteStatusAction.Decline:
                    newStatus = SiteStatusType.Locked;
                    break;
                case SiteStatusAction.RequestChange:
                case SiteStatusAction.Unapprove:
                    newStatus = SiteStatusType.Active;
                    break;
                default:
                    return false;
            }
            return SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);
        }
    }
}
