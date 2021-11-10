using System.Collections.Generic;

namespace Prime.ViewModels
{
    public class RemoteAccessSiteViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public SiteViewModel Site { get; set; }

        public class SiteViewModel
        {
            public int Id { get; set; }
            public int OrganizationId { get; set; }
            public string DoingBusinessAs { get; set; }
            public IEnumerable<VendorViewModel> SiteVendors { get; set; }
            public AddressViewModel PhysicalAddress { get; set; }
        }

        public class VendorViewModel
        {
            public int VendorCode { get; set; }
        }
    }
}
