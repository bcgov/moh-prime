using Prime.Models;

namespace Prime.Contracts
{
    public interface SendSiteSubmissionEmail
    {
        int SiteId { get; }
        int BusinessLicenceId { get; }
        CareSettingType CareSettingCode { get; }
    }
}
