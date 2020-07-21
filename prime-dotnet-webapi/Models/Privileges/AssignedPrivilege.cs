using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AssignedPrivilege")]
    public class AssignedPrivilege : BaseAuditable
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int PrivilegeId { get; set; }

        public Privilege Privilege { get; set; }
    }
}
