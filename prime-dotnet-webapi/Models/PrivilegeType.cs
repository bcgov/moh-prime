using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PrivilegeTypeLookup")]
    public class PrivilegeType : BaseAuditable, ILookup<short>
    {
        public readonly static short PHARMANET_TRANSACTIONS_TYPE = 2;

        [Key]
        public short Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<PrivilegeGroup> PrivilegeGroups { get; set; }
    }
}
