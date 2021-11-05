using System;
using System.Collections.Generic;

using Prime.Models;

namespace Prime.ViewModels.SiteRegistration.ReviewDocument
{
    public class SiteRegistrationReviewViewModel
    {
        public string OrganizationName { get; set; }
        public string OrganizationRegistrationId { get; set; }
        public string OrganizationDoingBusinessAs { get; set; }
        public int OrganizationReferenceId { get; set; }
        public string SiteName { get; set; }
        public string PEC { get; set; }
        public PhysicalAddress SiteAddress { get; set; }
        public IEnumerable<BusinessHourViewModel> BusinessHours { get; set; }
        public IEnumerable<VendorViewModel> Vendors { get; set; }
        public IEnumerable<RemoteUserViewModel> RemoteUsers { get; set; }
        public ContactViewModel SigningAuthority { get; set; }
        public ContactViewModel AdministratorOfPharmaNet { get; set; }
        public ContactViewModel PrivacyOfficer { get; set; }
        public ContactViewModel TechnicalSupport { get; set; }
    }

    public class BusinessHourViewModel
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class VendorViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class RemoteUserViewModel
    {
        public string FullName { get; set; }
        public IEnumerable<CertViewModel> Certifications { get; set; }
    }

    public class CertViewModel
    {
        public string CollegeName { get; set; }
        public string LicenceNumber { get; set; }
    }

    public class ContactViewModel
    {
        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string SmsPhone { get; set; }
        public string Email { get; set; }
    }
}
