using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CareTypes { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        public Contact PrivacyOfficer { get; set; }
        public Contact TechnicalSupport { get; set; }
        public Contact PharmanetAdministrator { get; set; }
    }
}
