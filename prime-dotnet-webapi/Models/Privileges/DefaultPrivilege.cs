using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("DefaultPrivilege")]
    public class DefaultPrivilege
    {
        public int LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }

        public int PrivilegeId { get; set; }

        [JsonIgnore]
        public Privilege Privilege { get; set; }

    }
}
