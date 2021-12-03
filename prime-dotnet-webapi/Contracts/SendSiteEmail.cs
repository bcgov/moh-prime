using Prime.Models;

namespace Prime.Contracts
{
    public interface SendSiteEmail
    {
        SiteEmailType EmailType { get; }
        CommunitySite Site { get; }
        string Note { get; }
    }

    public enum SiteEmailType
    {
        BusinessLicenceUploaded = 1,
        RemoteUserNotifications,
        RemoteUsersUpdated,
        SiteApprovedHIBC,
        SiteApprovedPharmaNetAdministrator,
        SiteApprovedSigningAuthority,
        SiteRegistrationSubmission,
        SiteActiveBeforeRegistration,
        SiteReviewedNotification
    }
}
