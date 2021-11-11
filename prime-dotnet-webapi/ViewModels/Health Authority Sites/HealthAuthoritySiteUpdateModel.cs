using System.Collections.Generic;
using Prime.ViewModels.Sites;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteUpdateModel
    {
        public string SiteName { get; set; }
        public string PEC { get; set; }
        public int? SecurityGroupCode { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
        public int HealthAuthorityVendorId { get; set; }
        public int? HealthAuthorityCareTypeId { get; set; }
        public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        public int? HealthAuthorityTechnicalSupportId { get; set; }
        public ICollection<BusinessDayViewModel> BusinessHours { get; set; }
    }
}
