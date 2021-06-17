using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("PrivacyOffice")]
    public class PrivacyOffice : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int HealthAuthorityOrganizationId { get; set; }

        [Required]
        public HealthAuthorityOrganization HealthAuthorityOrganization { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }
    }
}
