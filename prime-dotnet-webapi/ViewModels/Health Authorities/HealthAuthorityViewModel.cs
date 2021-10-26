using System.Collections.Generic;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CareTypes { get; set; }
        public IEnumerable<HealthAuthorityVendorViewModel> Vendors { get; set; }
        public PrivacyOfficeViewModel PrivacyOffice { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> TechnicalSupports { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> PharmanetAdministrators { get; set; }
    }
}
