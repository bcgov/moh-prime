using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteSubmissionViewModel
    {
        public string SiteName { get; set; }
        public AddressViewModel SiteAddress { get; set; }
        public string AuthorizedUserFullName { get; set; }
        public string HealthAuthorityName { get; set; }
        public string CareType { get; set; }
        public string PEC { get; set; }
        public string NewOrExisting
        {
            get
            {
                return string.IsNullOrEmpty(PEC)
                ? "New Site"
                : "Existing Site";
            }
        }
        public string VendorName { get; set; }
        public ContactViewModel PharmaNetAdministrator { get; set; }
    }
}
