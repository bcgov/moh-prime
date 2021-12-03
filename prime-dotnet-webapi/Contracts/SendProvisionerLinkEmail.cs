using System.Collections.Generic;

using Prime.Models;

namespace Prime.Contracts
{
    public interface SendProvisionerLinkEmail
    {
        IEnumerable<string> RecipientEmails { get; }
        int EnrolleeId { get; }
        string TokenUrl { get; }
        int CareSettingCode { get; }
    }
}
