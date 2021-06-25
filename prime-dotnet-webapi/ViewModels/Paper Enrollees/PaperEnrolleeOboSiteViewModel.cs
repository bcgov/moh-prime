using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeOboSiteViewModel
    {
        public int CareSettingCode { get; set; }
        public HealthAuthorityCode? HealthAuthorityCode { get; set; }
        public string SiteName { get; set; }
        public string PEC { get; set; }
        public string FacilityName { get; set; }
        public string JobTitle { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
