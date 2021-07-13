using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityVendor")]
    public class HealthAuthorityVendor : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [JsonIgnore]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        [Required]
        public int VendorCode { get; set; }

        [JsonIgnore]
        public Vendor Vendor { get; set; }
    }
}
