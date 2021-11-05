using System.Collections.Generic;

namespace Prime.ViewModels.Sites
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
