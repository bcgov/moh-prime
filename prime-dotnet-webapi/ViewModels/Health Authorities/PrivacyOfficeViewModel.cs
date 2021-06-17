namespace Prime.ViewModels.HealthAuthorities
{
    public class PrivacyOfficeViewModel
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public PrivacyOfficerViewModel PrivacyOfficer { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
