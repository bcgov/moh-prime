using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("CountryLookup")]
    public class Country : ILookup<string>
    {
        public const string CANADA = "CA";

        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
