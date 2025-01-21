using System;

namespace Prime.ViewModels
{
    public class UnlistedCertificationUpdateModel
    {
        public string CollegeName { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceClass { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
    }
}
