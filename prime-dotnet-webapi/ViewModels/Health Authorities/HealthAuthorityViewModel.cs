using System.Collections.Generic;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CareTypes { get; set; }
        public IEnumerable<int> VendorCodes { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> PrivacyOfficers { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> TechnicalSupports { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> PharmanetAdministrators { get; set; }
    }
}
