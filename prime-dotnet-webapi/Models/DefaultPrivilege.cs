using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("DefaultPrivilege")]
    public class DefaultPrivilege : BaseAuditable
    {
        public short LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }

        public int PrivilegeId { get; set; }

        [JsonIgnore]
        public Privilege Privilege { get; set; }

    }
}
