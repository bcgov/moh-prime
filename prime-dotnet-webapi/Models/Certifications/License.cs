using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseLookup")]
    public class License : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        public int Weight { get; set; }

        public string Prefix { get; set; }

        public bool Manual { get; set; }

        public bool Validate { get; set; }

        public bool NamedInImReg { get; set; }

        public bool LicensedToProvideCare { get; set; }

        public PrescriberIdType? PrescriberIdType { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        [JsonIgnore]
        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }
    }
}
