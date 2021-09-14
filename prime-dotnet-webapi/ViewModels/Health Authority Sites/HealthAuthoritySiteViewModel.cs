using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        // TODO should we use a relationship or direct data type?
        // public int HealthAuthorityVendorId { get; set; }
        public int VendorCode { get; set; }
        public string SiteName { get; set; }
        public string SiteId { get; set; }
        public int SecurityGroupCode { get; set; }
        // TODO should we use a relationship or direct data type?
        // public int HealthAuthorityCareTypeId { get; set; }
        public string CareType { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public ICollection<BusinessDay> BusinessHours { get; set; }
        public ICollection<RemoteUser> RemoteUsers { get; set; }
        public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        // TODO don't need the whole thing: first and last name for overview
        public HealthAuthorityPharmanetAdministrator HealthAuthorityPharmanetAdministrator { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
    }
}
