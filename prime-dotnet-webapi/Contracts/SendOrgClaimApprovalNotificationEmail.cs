using Prime.Models;

namespace Prime.Contracts
{
    public interface SendOrgClaimApprovalNotificationEmail
    {
        int OrganizationId { get; }
        int NewSigningAuthorityId { get; }
        string ProvidedSiteId { get; }
    }
}
