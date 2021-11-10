using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteSubmissionViewModel
    {
        public string SiteName { get; set; }
        public PhysicalAddress SiteAddress { get; set; }
        public string AuthorizedUserName { get; set; }
        public string HealthAuthorityName { get; set; }
        public string PEC { get; set; }
        public string CareType { get; set; }
        public string NewOrExisting { get; set; }
        public string Vendor { get; set; }
        public HealthAuthorityContactViewModel PharmaNetAdministrator { get; set; }
        
        public class HealthAuthorityContactViewModel
        {
            public string JobTitle { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string SmsPhone { get; set; }
            public string Email { get; set; }
        }
    }
}
