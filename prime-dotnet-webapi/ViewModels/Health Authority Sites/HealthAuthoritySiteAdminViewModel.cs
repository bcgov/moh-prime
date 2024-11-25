using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels.Sites;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteAdminViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public string SiteName { get; set; }
        public string Mnemonic { get; set; }
        public string PEC { get; set; }
        public int SecurityGroupCode { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
        public HealthAuthorityVendorViewModel HealthAuthorityVendor { get; set; }
        public HealthAuthorityCareTypeViewModel HealthAuthorityCareType { get; set; }
        public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        public int? HealthAuthorityTechnicalSupportId { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public SiteStatusType Status { get; set; }
        public string PharmanetAdministratorName { get; set; }
        public string TechnicalSupportName { get; set; }
        public IEnumerable<BusinessDayViewModel> BusinessHours { get; set; }
        public SiteSubmission CurrentSubmission { get; set; }
    }
}
