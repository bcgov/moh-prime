using Prime.Models.Api;
using Prime.Engines;
using Prime.Models;

namespace Prime.Services
{
    public class SiteRegistrationService : ISiteRegistrationService
    {
        public bool CanPerformSiteStatusAction(SiteStatusAction action)
        {
            SiteStatusType fromStatus, toStatus;

            switch (action)
            {
                case SiteStatusAction.Submit:
                    fromStatus = SiteStatusType.Active;
                    toStatus = SiteStatusType.InReview;
                    break;
                case SiteStatusAction.Approve:
                    fromStatus = SiteStatusType.InReview;
                    toStatus = SiteStatusType.Approved;
                    break;
                case SiteStatusAction.Decline:
                    fromStatus = SiteStatusType.InReview;
                    toStatus = SiteStatusType.Locked;
                    break;
                case SiteStatusAction.RequestChange:
                    fromStatus = SiteStatusType.InReview;
                    toStatus = SiteStatusType.Active;
                    break;
                case SiteStatusAction.Unapprove:
                    fromStatus = SiteStatusType.Approved;
                    toStatus = SiteStatusType.Active;
                    break;
                case SiteStatusAction.Undecline:
                    fromStatus = SiteStatusType.Locked;
                    toStatus = SiteStatusType.InReview;
                    break;
                default:
                    return false;
            }
            return SiteStatusStateEngine.AllowableStatusChange(fromStatus, toStatus);
        }
    }
}
