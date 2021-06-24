using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("CollegeLicenseGroupingLookup")]
    public class CollegeLicenseGrouping : ILookup<int>
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
