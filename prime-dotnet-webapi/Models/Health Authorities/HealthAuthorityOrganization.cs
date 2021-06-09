using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using DelegateDecompiler;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityOrganization")]
    public class HealthAuthorityOrganization : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<HealthAuthorityCareType> CareTypes { get; set; }

        public ICollection<HealthAuthorityVendor> Vendors { get; set; }

        public int PrivacyOfficerId { get; set; }

        public Contact PrivacyOfficer { get; set; }

        public ICollection<HealthAuthorityTechnicalSupport> TechnicalSupports { get; set; }

        public ICollection<HealthAuthorityPharmanetAdministrator> PharmanetAdministrators { get; set; }
    }
}
