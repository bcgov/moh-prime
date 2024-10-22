using System;
using System.Collections.Generic;

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
