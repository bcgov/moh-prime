using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.HealthAuthorities;
using System.Collections.Generic;

using Prime.ViewModels.Sites;
namespace Prime.Models
{
    [Table("V2HealthAuthoritySite")]
    public class V2HealthAuthoritySite : Site
    {
        public string SiteName { get; set; }

        public string SiteId { get; set; }

        public int SecurityGroupCode { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        // TODO: Note Change name from organization to HealthAuthorityOrganization
        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public int? HealthAuthorityVendorId { get; set; }

        [JsonIgnore]
        public HealthAuthorityVendor HealthAuthorityVendor { get; set; }

        public int? HealthAuthorityCareTypeId { get; set; }

        public HealthAuthorityCareType HealthAuthorityCareType { get; set; }

        public int? HealthAuthorityPharmanetAdministratorId { get; set; }

        public HealthAuthorityPharmanetAdministrator HealthAuthorityPharmanetAdministrator { get; set; }

        public int? HealthAuthorityTechnicalSupportId { get; set; }

        public HealthAuthorityTechnicalSupport HealthAuthorityTechnicalSupport { get; set; }

        public int? AuthorizedUserId { get; set; }

        public Party AuthorizedUser { get; set; }
        public new ICollection<BusinessDayViewModel> BusinessHours { get; set; }

        public new ICollection<RemoteUserViewModel> RemoteUsers { get; set; }

    }
}
