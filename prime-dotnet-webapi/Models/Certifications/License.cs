using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseLookup")]
    public class License : BaseAuditable, ILookup<short>
    {
        [Key]
        public short Code { get; set; }

        public int Weight { get; set; }

        [Required]
        public bool Manual { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }
    }
}
