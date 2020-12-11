using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{

    [Table("HealthAuthorityLookup")]
    public class HealthAuthority: BaseAuditable, ILookup<HealthAuthorityCode>
    {
        [Key]
        public HealthAuthorityCode Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
