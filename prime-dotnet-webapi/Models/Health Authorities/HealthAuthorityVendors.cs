using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityVendor")]
    public class HealthAuthorityVendor : BaseAuditable
    {
        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public int VendorCode { get; set; }

        [JsonIgnore]
        public Vendor Vendor { get; set; }
    }
}
