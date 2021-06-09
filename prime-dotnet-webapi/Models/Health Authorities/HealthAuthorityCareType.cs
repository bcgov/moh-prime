using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("HealthAuthorityCareType")]

    public class HealthAuthorityCareType : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [Required]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        [Required]
        public string CareType { get; set; }
    }
}
