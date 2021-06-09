using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CareTypes { get; set; }
        public IEnumerable<int> VendorCodes { get; set; }
        public ContactViewModel PrivacyOfficer { get; set; }
        public ContactViewModel TechnicalSupport { get; set; }
        public ContactViewModel PharmanetAdministrator { get; set; }
    }
}
