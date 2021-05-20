using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("FacilityLookup")]
    public class Facility : ILookup<FacilityCode>
    {
        [Key]
        public FacilityCode Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
