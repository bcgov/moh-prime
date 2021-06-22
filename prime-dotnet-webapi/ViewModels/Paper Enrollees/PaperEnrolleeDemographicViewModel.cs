using System;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeDemographicViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string SmsPhone { get; set; }
    }
}
