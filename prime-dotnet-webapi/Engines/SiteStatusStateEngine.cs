using Prime.Models;
using Prime.Models.Api;

namespace Prime.Engines
{
    public static class SiteStatusStateEngine
    {
        public static bool AllowableStatusChange(SiteStatusType fromStatus, SiteStatusType toStatus)
        {
            return (fromStatus, toStatus) switch
            {
                // Submit
                (SiteStatusType.Active, SiteStatusType.UnderReview) => true,
                // Reject
                (SiteStatusType.Active, SiteStatusType.Declined) => true,
                (SiteStatusType.UnderReview, SiteStatusType.Declined) => true,
                // Changes requested
                (SiteStatusType.UnderReview, SiteStatusType.Active) => true,
                // Approve
                (SiteStatusType.UnderReview, SiteStatusType.Approved) => true,
                // Unapprove for changes
                (SiteStatusType.Approved, SiteStatusType.Active) => true,
                // Unreject
                (SiteStatusType.Declined, SiteStatusType.UnderReview) => true,

                _ => false
            };
        }
    }
}
