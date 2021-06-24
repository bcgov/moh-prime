using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.HealthAuthorities
{
    [Table("CareTypeLookup")]
    public class CareType : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        public string Name { get; set; }
    }
}
