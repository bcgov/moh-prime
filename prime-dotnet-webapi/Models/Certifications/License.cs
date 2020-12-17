using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseLookup")]
    public class License : BaseAuditable, ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        public int Weight { get; set; }

        [JsonIgnore]
        public bool Manual { get; set; }

        public bool Validate { get; set; }

        public bool NamedInImReg  { get; set; }

        public bool LicensedToProvideCare { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        [JsonIgnore]
        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }
    }
}
