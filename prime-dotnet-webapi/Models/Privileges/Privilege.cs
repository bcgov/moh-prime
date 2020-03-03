using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Privilege")]
    public class Privilege : BaseAuditable
    {
        public const int RU_CODE = 17;
        public const int OBO_CODE = 18;


        [Key]
        public int Id { get; set; }

        public string TransactionType { get; set; }

        public string Description { get; set; }

        public int PrivilegeGroupCode { get; set; }

        [JsonIgnore]
        public PrivilegeGroup PrivilegeGroup { get; set; }

        [JsonIgnore]
        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges { get; set; }

    }
}
