using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("PrivilegeTypeLookup")]
    public class PrivilegeType : BaseAuditable, ILookup<short>
    {
        [Key]
        public short Code { get; set; }

        public string Name { get; set; }

        public ICollection<PrivilegeGroup> PrivilegeGroups { get; set; }
    }
}
