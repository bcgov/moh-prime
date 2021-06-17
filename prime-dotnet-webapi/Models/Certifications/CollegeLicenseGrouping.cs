using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("CollegeLicenseGrouping")]
    public class CollegeLicenseGrouping
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
