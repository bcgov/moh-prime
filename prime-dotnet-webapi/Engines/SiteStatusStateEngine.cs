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
                (SiteRegistrationAction.Submit, SiteStatusType.Editable) => true,
                // Reject
                (SiteRegistrationAction.Reject, SiteStatusType.InReview) => true,
                (SiteRegistrationAction.Reject, SiteStatusType.Editable) => true,
                // Changes requested
                (SiteRegistrationAction.RequestChange, SiteStatusType.InReview) => true,
                // Approve
                (SiteRegistrationAction.Approve, SiteStatusType.InReview) => true,
                // Unreject
                (SiteRegistrationAction.Unreject, SiteStatusType.InReview) => true,

                _ => false
            };
        }
    }
}
