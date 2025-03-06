namespace Prime.Models.Api
{
    public class OrganizationSearchOptions
    {
        public string TextSearch { get; set; }
        public int? CareSettingCode { get; set; }
        public int? Page { get; set; }
        public int? Status { get; set; }
        public int? OrganizationId { get; set; }
    }
}
