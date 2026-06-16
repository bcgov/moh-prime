namespace Prime.Models.Api
{
    public class EmailsForCareSetting
    {
        public ProvisionerEmail[] Emails { get; set; }
        public int CareSettingCode { get; set; }
        public int? HealthAuthorityCode { get; set; }
    }

    public class ProvisionerEmail
    {
        public string Email { get; set; }
        public int[] RemoteAccessSiteIds { get; set; }
    }
}
