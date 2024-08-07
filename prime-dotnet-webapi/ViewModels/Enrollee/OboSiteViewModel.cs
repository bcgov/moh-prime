using Prime.Models;

namespace Prime.ViewModels
{
    public class OboSiteViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public int CareSettingCode { get; set; }
        public HealthAuthorityCode? HealthAuthorityCode { get; set; }
        public string SiteName { get; set; }
        public string FacilityName { get; set; }
        public string JobTitle { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
