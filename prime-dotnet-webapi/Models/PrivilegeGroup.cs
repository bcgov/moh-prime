using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class PrivilegeGroup : BaseAuditable, ILookup<short>
    {
        [Key]
        public short Code { get; set; }

        public short PrivilegeTypeCode { get; set; }

        public PrivilegeType PrivilegeType { get; set; }

        public string Name { get; set; }

        public ICollection<Privilege> Privileges { get; set; }
    }
}
