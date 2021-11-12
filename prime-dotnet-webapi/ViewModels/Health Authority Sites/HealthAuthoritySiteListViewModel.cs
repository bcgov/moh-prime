using System;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteListViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public string HealthAuthorityName { get; set; }
        public string SiteName { get; set; }
        public string PEC { get; set; }
        public string Flagged { get; set; }
        public HealthAuthorityVendorViewModel HealthAuthorityVendor { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public SiteStatusType Status { get; set; }
        public string AuthorizedUserName { get; set; }
        public string AuthorizedUserEmail { get; set; }
    }
}
