using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityContact")]
    public abstract class HealthAuthorityContact : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public int ContactId { get; set; }

        [JsonIgnore]
        public Contact Contact { get; set; }
    }

    public class HealthAuthorityPrivacyOfficer : HealthAuthorityContact { }

    public class HealthAuthorityTechnicalSupport : HealthAuthorityContact
    {
        public ICollection<HealthAuthorityTechnicalSupportVendor> VendorsSupported { get; set; }
    }

    public class HealthAuthorityPharmanetAdministrator : HealthAuthorityContact { }
}
