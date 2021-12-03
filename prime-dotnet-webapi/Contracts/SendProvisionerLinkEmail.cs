using System.Collections.Generic;

using Prime.Models;

namespace Prime.Contracts
{
    public interface SendProvisionerLinkEmail
    {
        IEnumerable<string> RecipientEmails { get; }
        EnrolmentCertificateAccessToken EnrolmentCertificateAccessToken { get; }
        int CareSettingCode { get; }
    }
}
