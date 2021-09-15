using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("SecurityGroupLookup")]
    public class SecurityGroup : ILookup<SecurityGroupCode>
    {
        [Key]
        public SecurityGroupCode Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
