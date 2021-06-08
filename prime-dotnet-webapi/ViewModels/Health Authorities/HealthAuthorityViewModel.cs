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
        public Contact PrivacyOffice { get; set; }
        public IEnumerable<Contact> TechnicalSupports { get; set; }
        public IEnumerable<Contact> PharmanetAdministrators { get; set; }
    }
}
