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
        public HealthAuthorityCode Id { get; set; }

        public string Name { get; set; }

        public ICollection<HealthAuthorityCareType> CareTypes { get; set; }

        public Contact PrivacyOffice { get; set; }
    }
}
