using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{
    [Table("CountryLookup")]
    public class Country : IDefinable, ILookup<string>
    {
        [Key]
        public string Code ,

        [Required]
        public string Name ,
    }
}
