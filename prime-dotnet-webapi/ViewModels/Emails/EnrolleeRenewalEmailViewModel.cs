using System;

namespace Prime.ViewModels.Emails
{
    public class EnrolleeRenewalEmailViewModel
    {
        public string EnrolleeName { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
        public string PrimeUrl { get; set; }
    }
}
