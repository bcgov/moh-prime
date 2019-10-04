using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("OrganizationNameLookup")]
    public class OrganizationName : ILookup
    {
        [Key]
        public short Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}