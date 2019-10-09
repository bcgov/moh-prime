using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("CollegeLookup")]
    public class College : ILookup
    {
        [Key]
        public short Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Prefix { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }
    }
}