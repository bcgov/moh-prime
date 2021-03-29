namespace Prime.ViewModels
{
    public class RemoteAccessSiteViewModel
    {
        public int OrganizationId { get; set; }
        public int SiteId { get; set; }
        public string DoingBusinessAs { get; set; }
        public string FacilityName { get; set; }
        public int VendorCode { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
