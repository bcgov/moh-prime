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
        public int VendorId { get; set; }
        public int? CareTypeId { get; set; }
        public int? PharmanetAdministratorId { get; set; }
        public int? TechnicalSupportId { get; set; }
    }
}
