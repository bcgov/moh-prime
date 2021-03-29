using System;
using System.Collections.Generic;

namespace Prime.ViewModels
{
    public class EnrolleeOverviewViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public AddressViewModel VerifiedAddress { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
        public AddressViewModel MailingAddress { get; set; }
        public string Phone { get; set; }
        public string PhoneExtension { get; set; }
        public string Email { get; set; }
        public string SmsPhone { get; set; }
        public IEnumerable<int> CareSettings { get; set; }
        public IEnumerable<CertificationViewModel> Certifications { get; set; }
        public IEnumerable<string> Jobs { get; set; }
        public IEnumerable<OboSiteViewModel> OboSites { get; set; }
        public IEnumerable<RemoteAccessSiteViewModel> RemoteAccessSites { get; set; }
        public IEnumerable<RemoteAccessLocationViewModel> RemoteAccessLocations { get; set; }
        public IEnumerable<SelfDeclarationViewModel> SelfDeclarations { get; set; }
    }
}
