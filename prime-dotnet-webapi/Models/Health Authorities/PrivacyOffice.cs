namespace Prime.Models.HealthAuthorities
{
    public class PrivacyOffice : BaseAuditable
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
    }
}
