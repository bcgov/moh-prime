using System;

namespace Prime.ViewModels.Emails
{
    public class EnrolleeRenewalEmailViewModel
    {
        public string EnrolleeName { get; set; }
        public DateTimeOffset RenewalDate { get; set; }
        public string PrimeUrl { get; set; }

        public EnrolleeRenewalEmailViewModel(string firstName, string lastName, DateTimeOffset renewalDate, string gpId = "")
        {
            EnrolleeName = $"{firstName} {lastName}";
            RenewalDate = renewalDate;
            PrimeUrl = PrimeEnvironment.FrontendUrl;

        }
    }
}
