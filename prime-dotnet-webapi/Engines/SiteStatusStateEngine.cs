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
                (SiteStatusType.Active, SiteStatusType.InReview) => true,
                // Reject
                (SiteStatusType.Active, SiteStatusType.Locked) => true,
                (SiteStatusType.InReview, SiteStatusType.Locked) => true,
                // Changes requested
                (SiteStatusType.InReview, SiteStatusType.Active) => true,
                // Approve
                (SiteStatusType.InReview, SiteStatusType.Approved) => true,
                // Unapprove for changes
                (SiteStatusType.Approved, SiteStatusType.Active) => true,
                // Unreject
                (SiteStatusType.Locked, SiteStatusType.InReview) => true,

                _ => false
            };
        }
    }
}
