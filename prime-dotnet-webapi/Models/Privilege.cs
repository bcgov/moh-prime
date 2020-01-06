using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class Privilege : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string TransactionType { get; set; }

        public string Description { get; set; }

        public int PrivilegeGroupId { get; set; }

        [JsonIgnore]
        public PrivilegeGroup PrivilegeGroup { get; set; }

        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }

        public ICollection<AssignedPrivilege> AssignedPrivileges { get; set; }

    }
}
