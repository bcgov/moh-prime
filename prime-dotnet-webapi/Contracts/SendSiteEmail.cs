using System.Collections.Generic;

namespace Prime.Contracts
{
    public interface SendSiteEmail
    {
        SiteEmailType EmailType { get; }
        int Id { get; }
        int BusinessLicenceId { get; }
        int CareSettingCode { get; }
        string DoingBusinessAs { get; }
        string PhysicalAddressStreet { get; }
        string PhysicalAddressCity { get; }
        string OrganizationName { get; }
        string AdministratorPharmaNetEmail { get; }
        string AdjudicatorEmail { get; }
        string ProvisionerEmail { get; }
        string OrganizationSigningAuthorityEmail { get; }
        string PEC { get; }
        IEnumerable<string> RemoteUserNames { get; }
        IEnumerable<string> RemoteUserEmails { get; }
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
