using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityCareTypeToVendor")]
    public class HealthAuthorityCareTypeToVendor : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityCareTypeId { get; set; }

        [JsonIgnore]
        public HealthAuthorityCareType HealthAuthorityCareType { get; set; }

        public int HealthAuthorityVendorId { get; set; }

        [JsonIgnore]
        public HealthAuthorityVendor HealthAuthorityVendor { get; set; }

        public DateTime? DeletedDateTime { get; set; }
    }
}
