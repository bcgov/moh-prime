using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityCareType")]

    public class HealthAuthorityCareType : BaseAuditable
    {
        [Required]
        public HealthAuthority HealthAuthority { get; set; }

        [Required]
        public string CareType { get; set; }
    }
}
