using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("ProvinceLookup")]
    public class Province : BaseAuditable, ILookup<string>
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
