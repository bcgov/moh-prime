using Prime.Models;
using Prime.Models.Api;

namespace Prime.Engines
{
    public static class SiteStatusStateEngine
    {
        public static bool AllowableStatusChange(SiteRegistrationAction action, SiteStatusType currentStatus)
        {
            return (action, currentStatus) switch
            {
                // Submit
                (SiteRegistrationAction.Submit, SiteStatusType.Active) => true,
                // Reject
                (SiteRegistrationAction.Decline, SiteStatusType.InReview) => true,
                (SiteRegistrationAction.Decline, SiteStatusType.Active) => true,
                // Changes requested
                (SiteRegistrationAction.RequestChange, SiteStatusType.InReview) => true,
                // Approve
                (SiteRegistrationAction.Approve, SiteStatusType.InReview) => true,
                // Unapprove for changes
                (SiteRegistrationAction.Unapprove, SiteStatusType.Approved) => true,
                // Unreject
                (SiteRegistrationAction.Undecline, SiteStatusType.Locked) => true,

                _ => false
            };
        }
    }
}
