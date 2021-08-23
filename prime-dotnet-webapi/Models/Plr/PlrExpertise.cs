
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.Plr
{
    [Table("PlrExpertiseLookup")]
    public class PlrExpertise : ILookup<string>
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
