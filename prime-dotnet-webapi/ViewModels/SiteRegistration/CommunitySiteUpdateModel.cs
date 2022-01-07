using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prime.Models;

namespace Prime.ViewModels
{
    public class CommunitySiteUpdateModel
    {
        [Key]
        public int Id { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public int? AdministratorPharmaNetId { get; set; }

        public Contact AdministratorPharmaNet { get; set; }

        public int? PrivacyOfficerId { get; set; }

        public Contact PrivacyOfficer { get; set; }

        public int? TechnicalSupportId { get; set; }

        public Contact TechnicalSupport { get; set; }

        public int? CareSettingCode { get; set; }

        public string PEC { get; set; }

        public string DoingBusinessAs { get; set; }

        public bool ActiveBeforeRegistration { get; set; }

        public IEnumerable<SiteVendor> SiteVendors { get; set; }

        public IEnumerable<SiteRemoteUserUpdateModel> RemoteUsers { get; set; }

        public ICollection<BusinessDay> BusinessHours { get; set; }

        public IEnumerable<IndividualDeviceProviderChangeModel> IndividualDeviceProviders { get; set; }
    }
}
