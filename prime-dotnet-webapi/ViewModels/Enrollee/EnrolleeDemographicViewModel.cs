using System;

namespace Prime.ViewModels
{
    public class EnrolleeDemographicViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenNames { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string SmsPhone { get; set; }
        public string Phone { get; set; }
        public string PhoneExtension { get; set; }

        public AddressViewModel VerifiedAddress { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
        public AddressViewModel MailingAddress { get; set; }

        public string GPID { get; set; }
    }
}
