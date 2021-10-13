using System;

namespace Prime.ViewModels.Parties
{
    public class PartyCertificationViewModel
    {
        public int CollegeCode { get; set; }

        public int LicenseCode { get; set; }

        public string LicenseNumber { get; set; }

        public string PractitionerId { get; set; }

        public DateTimeOffset RenewalDate { get; set; }

        public int? PracticeCode { get; set; }
    }
}
