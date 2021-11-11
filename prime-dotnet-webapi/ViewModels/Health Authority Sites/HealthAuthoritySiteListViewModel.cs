using System;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteListViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public string SiteName { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public string AdjudicatorIdir { get; set; }
        public SiteStatusType Status { get; set; }
        public string PEC { get; set; }
    }
}
