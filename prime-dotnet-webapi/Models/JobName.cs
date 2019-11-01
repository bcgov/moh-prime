using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("JobNameLookup")]
    public class JobName : BaseAuditable, ILookup
    {
        [Key]
        public short Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
