using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PrivilegeTypeLookup")]
    public class PrivilegeType : ILookup<int>
    {
        public readonly static int PHARMANET_TRANSACTIONS = 2;

        [Key]
        public int Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<PrivilegeGroup> PrivilegeGroups { get; set; }
    }
}
