using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("DefaultPrivilege")]
    public class DefaultPrivilege : IDefinable
    {
        public short LicenseCode ,

        [JsonIgnore]
        public License License ,

        public int PrivilegeId ,

        [JsonIgnore]
        public Privilege Privilege ,

    }
}
