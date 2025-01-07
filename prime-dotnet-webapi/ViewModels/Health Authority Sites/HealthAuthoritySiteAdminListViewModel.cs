using System;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteAdminListViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public string HealthAuthorityName { get; set; }
        public string SiteName { get; set; }
        public string Mnemonic { get; set; }
        public string PEC { get; set; }
        public bool Flagged { get; set; }
        public HealthAuthorityVendorViewModel HealthAuthorityVendor { get; set; }
        public HealthAuthorityCareTypeViewModel HealthAuthorityCareType { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public SiteStatusType Status { get; set; }
        public string AuthorizedUserName { get; set; }
        public string AuthorizedUserEmail { get; set; }
        public string AdjudicatorIdir { get; set; }
        public bool HasNotification { get; set; }
        public SiteSubmission CurrentSubmission { get; set; }
        public int DuplicatePecSiteCount { get; set; }
    }
}
