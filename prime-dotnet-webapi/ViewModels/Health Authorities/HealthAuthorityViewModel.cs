using System.Collections.Generic;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CareTypes { get; set; }
        public IEnumerable<int> VendorCodes { get; set; }
        public PrivacyOfficeViewModel PrivacyOffice { get; set; }
        public IEnumerable<ContactViewModel> TechnicalSupports { get; set; }
        public IEnumerable<ContactViewModel> PharmanetAdministrators { get; set; }
    }
}
