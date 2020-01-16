using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PrivilegeGroupLookup")]
    public class PrivilegeGroup : BaseAuditable, ILookup<short>
    {
        public readonly static short USER_TYPE = 4;
        public readonly static short CAN_HAVE_OBOS = 5;


        [Key]
        public short Code { get; set; }

        public short PrivilegeTypeCode { get; set; }

        [JsonIgnore]
        public PrivilegeType PrivilegeType { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Privilege> Privileges { get; set; }
    }
}
