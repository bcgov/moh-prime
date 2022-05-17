using System;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeUnlistedCertificationViewModel
    {
        public string UnlistedCollegeName { get; set; }
        public int UnlistedCollegeCode { get; set; }
        public DateTimeOffset UnlistedRenewalDate { get; set; }
    }
}
