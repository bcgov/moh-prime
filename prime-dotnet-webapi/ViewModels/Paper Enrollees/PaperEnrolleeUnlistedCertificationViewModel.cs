using System;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeUnlistedCertificationViewModel
    {
        public string CollegeName { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceClass { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
    }
}
