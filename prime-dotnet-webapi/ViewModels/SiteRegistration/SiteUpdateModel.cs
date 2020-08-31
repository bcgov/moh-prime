using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteUpdateModel
    {
        [Key]
        public int Id { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public int? AdministratorPharmaNetId { get; set; }

        public Party AdministratorPharmaNet { get; set; }

        public int? PrivacyOfficerId { get; set; }

        public Party PrivacyOfficer { get; set; }

        public int? TechnicalSupportId { get; set; }

        public Party TechnicalSupport { get; set; }

        public int? CareSettingCode { get; set; }

        public string DoingBusinessAs { get; set; }

        public IEnumerable<SiteVendor> SiteVendors { get; set; }

        public IEnumerable<RemoteUser> RemoteUsers { get; set; }

        public ICollection<BusinessDay> BusinessHours { get; set; }
    }
}
