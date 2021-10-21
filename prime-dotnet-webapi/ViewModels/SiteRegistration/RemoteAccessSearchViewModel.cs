using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class RemoteAccessSearchViewModel
    {
        public int RemoteUserId { get; set; }
        public int SiteId { get; set; }
        public string SiteDoingBusinessAs { get; set; }
        public AddressViewModel SiteAddress { get; set; }
        public IEnumerable<int> VendorCodes { get; set; }
    }
}
