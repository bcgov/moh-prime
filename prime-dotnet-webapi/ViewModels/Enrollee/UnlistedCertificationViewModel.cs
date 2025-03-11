using System;

namespace Prime.ViewModels
{
    public class UnlistedCertificationViewModel
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceClass { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
    }
}
