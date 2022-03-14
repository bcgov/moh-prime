using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models.HealthAuthorities
{
    /// <summary>
    /// Associative entity/table between HealthAuthorityTechnicalSupport and
    /// Vendors they support.
    /// </summary>
    [Table("HealthAuthorityTechnicalSupportVendor")]
    public class HealthAuthorityTechnicalSupportVendor : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HealthAuthorityTechnicalSupportId { get; set; }

        [JsonIgnore]
        public HealthAuthorityTechnicalSupport HealthAuthorityTechnicalSupport { get; set; }

        [Required]
        public int HealthAuthorityVendorId { get; set; }

        [JsonIgnore]
        public HealthAuthorityVendor HealthAuthorityVendor { get; set; }
    }
}