using System;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteSubmissionViewModel
    {
        public string SiteName { get; set; }
        public PhysicalAddress SiteAddress { get; set; }
        public AuthorizedUserViewModel AuthorizedUser { get; set; }
        public HealthAuthorityViewModel HealthAuthority { get; set; }
        public string PEC { get; set; }
        public string CareType { get; set; }
        public bool IsNew { get; set; }
        public int Vendor { get; set; }
        public int? HealthAuthorityPharmanetAdministratorId { get; set; }
        public HealthAuthorityPharmanetAdministratorViewModel PharmaNetAdministrator { get; set; }

        public class AuthorizedUserViewModel
        {
            public string Name { get; set; }
        }
        public class HealthAuthorityViewModel
        {
            public string Name { get; set; }
        }
        public class HealthAuthorityPharmanetAdministratorViewModel
        {
            public string Name { get; set; }
        }
    }

}
