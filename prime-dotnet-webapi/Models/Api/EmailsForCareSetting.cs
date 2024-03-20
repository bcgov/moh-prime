namespace Prime.Models.Api
{
    public class EmailsForCareSetting
    {
        public string[] Emails { get; set; }
        public int CareSettingCode { get; set; }
        public int? HealthAuthorityCode { get; set; }
    }
}
