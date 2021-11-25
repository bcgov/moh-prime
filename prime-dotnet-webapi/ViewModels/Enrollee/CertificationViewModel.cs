using System;

namespace Prime.ViewModels
{
    public class CertificationViewModel
    {
        public int Id { get; set; }
        public int CollegeCode { get; set; }
        public int LicenseCode { get; set; }
        public string LicenseNumber { get; set; }
        public int? PracticeCode { get; set; }
        public string PractitionerId { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
    }
}
