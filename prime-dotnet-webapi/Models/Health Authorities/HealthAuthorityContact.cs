using Prime.Models;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityContact")]
    public abstract class HealthAuthorityContact : BaseAuditable
    {
        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public int ContactId { get; set; }

        [JsonIgnore]
        public Contact Contact { get; set; }
    }

    public class HealthAuthorityPrivacyOfficer : HealthAuthorityContact { }
    public class HealthAuthorityTechnicalSupport : HealthAuthorityContact { }
    public class HealthAuthorityPharmanetAdministrator : HealthAuthorityContact { }
}
