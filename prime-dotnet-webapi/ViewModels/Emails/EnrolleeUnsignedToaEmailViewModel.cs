using System;

namespace Prime.ViewModels.Emails
{
    public class EnrolleeUnsignedToaEmailViewModel
    {
        public string EnrolleeName { get; set; }
        public string PrimeUrl { get; set; }

        public EnrolleeUnsignedToaEmailViewModel(string firstName, string lastName)
        {
            EnrolleeName = $"{firstName} {lastName}";
        }
    }
}
