using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

using Prime.Models.HealthAuthorities;

namespace Prime.Models
{
    [Table("HealthAuthoritySite")]
    public class HealthAuthoritySite : Site
    {
        public string SiteName { get; set; }

        public int SecurityGroupCode { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public int? HealthAuthorityVendorId { get; set; }

        public HealthAuthorityVendor HealthAuthorityVendor { get; set; }

        public int? HealthAuthorityCareTypeId { get; set; }

        public HealthAuthorityCareType HealthAuthorityCareType { get; set; }

        public int? HealthAuthorityPharmanetAdministratorId { get; set; }

        public HealthAuthorityPharmanetAdministrator HealthAuthorityPharmanetAdministrator { get; set; }

        public int? HealthAuthorityTechnicalSupportId { get; set; }

        public HealthAuthorityTechnicalSupport HealthAuthorityTechnicalSupport { get; set; }

        public int AuthorizedUserId { get; set; }

        public AuthorizedUser AuthorizedUser { get; set; }
    }
}
