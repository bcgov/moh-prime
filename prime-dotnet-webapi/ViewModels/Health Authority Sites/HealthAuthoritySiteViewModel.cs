using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteViewModel
    {
        public int Id { get; set; }
        public int HealthAuthorityOrganizationId { get; set; }
        public int VendorCode { get; set; }
        public string SiteName { get; set; }
        public string SiteId { get; set; }
        public string SecurityGroup { get; set; }
        public string CareType { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public ICollection<BusinessDay> BusinessHours { get; set; }
        public ICollection<RemoteUser> RemoteUsers { get; set; }
        public int SiteAdministratorId { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
    }
}
