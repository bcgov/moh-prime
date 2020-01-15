using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{
    [Table("JobNameLookup")]
    public class JobName : IDefinable, ILookup<short>
    {
        [Key]
        public short Code ,

        [Required]
        public string Name ,
    }
}
