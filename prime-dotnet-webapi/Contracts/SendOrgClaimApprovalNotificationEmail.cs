using Prime.Models;

namespace Prime.Contracts
{
    public interface SendOrgClaimApprovalNotificationEmail
    {
        OrganizationClaim OrganizationClaim { get; }
    }
}
