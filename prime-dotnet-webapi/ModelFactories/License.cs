using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("LicenseLookup")]
    public class License : IDefinable, ILookup<short>
    {
        [Key]
        public short Code ,

        [Required]
        public string Name ,

        [JsonIgnore]
        public ICollection<Certification> Certifications ,

        public ICollection<CollegeLicense> CollegeLicenses ,

        public ICollection<DefaultPrivilege> DefaultPrivileges ,
    }
}
