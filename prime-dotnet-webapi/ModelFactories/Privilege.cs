using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{
    [Table("Privilege")]
    public class Privilege : IDefinable
    {
        [Key]
        public int Id ,

        public string TransactionType ,

        public string Description ,

        public int PrivilegeGroupId ,

        [JsonIgnore]
        public PrivilegeGroup PrivilegeGroup ,

        [JsonIgnore]
        public ICollection<DefaultPrivilege> DefaultPrivileges ,

        [JsonIgnore]
        public ICollection<AssignedPrivilege> AssignedPrivileges ,

    }
}
