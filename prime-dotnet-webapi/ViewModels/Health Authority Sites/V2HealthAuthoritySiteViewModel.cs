using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class V2HealthAuthoritySiteViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public string SiteName { get; set; }
        public string SiteId { get; set; }
        public int SecurityGroupCode { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
        public int HealthAuthorityVendorId { get; set; }
        public HealthAuthorityCareTypeViewModel CareType { get; set; }

        // TODO used?
        public bool Completed { get; set; }


        // public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        // public int? HealthAuthorityTechnicalSupportId { get; set; }
        // public ICollection<BusinessDay> BusinessHours { get; set; }
        // public ICollection<RemoteUser> RemoteUsers { get; set; }
    }
}
