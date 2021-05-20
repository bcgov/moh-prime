using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PrivilegeGroupLookup")]
    public class PrivilegeGroup : ILookup<int>
    {
        public readonly static int USER_TYPE = 4;
        public readonly static int CAN_HAVE_OBOS = 5;


        [Key]
        public int Code { get; set; }

        public int PrivilegeTypeCode { get; set; }

        [JsonIgnore]
        public PrivilegeType PrivilegeType { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Privilege> Privileges { get; set; }
    }
}
