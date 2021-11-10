using System.Collections.Generic;

namespace Prime.ViewModels.HealthAuthoritySites.Internal
{

    public class HealthAuthoritySelectionDto
    {
        public IEnumerable<int> VendorIds { get; set; }
        public IEnumerable<int> CareTypeIds { get; set; }
        public IEnumerable<int> PharmanetAdministratorIds { get; set; }
        public IEnumerable<int> TechnicalSupportIds { get; set; }
    }

    public class SiteSelectionDto
    {
        public int HealthAuthorityVendorId { get; set; }
        public int? HealthAuthorityCareTypeId { get; set; }
        public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        public int? HealthAuthorityTechnicalSupportId { get; set; }
    }
}
