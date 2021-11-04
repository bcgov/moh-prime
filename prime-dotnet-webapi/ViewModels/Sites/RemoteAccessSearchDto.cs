using System.Collections.Generic;

namespace Prime.ViewModels.Sites
{
    public class RemoteAccessSearchDto
    {
        public int RemoteUserId { get; set; }
        public int SiteId { get; set; }
        public string SiteDoingBusinessAs { get; set; }
        public AddressViewModel SiteAddress { get; set; }
        public IEnumerable<int> CommunityVendorCodes { get; set; }
        public int? HealthAuthorityVendorCode { get; set; }
        public IEnumerable<int> VendorCodes
        {
            get => HealthAuthorityVendorCode.HasValue
                ? new[] { HealthAuthorityVendorCode.Value }
                : CommunityVendorCodes;
        }
    }
}
