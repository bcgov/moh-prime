using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("CollegeLookup")]
    public class College : IDefinable, ILookup<short>
    {
        [Key]
        public short Code ,

        [Required]
        public string Name ,

        public string Prefix ,

        [JsonIgnore]
        public ICollection<Certification> Certifications ,

        public ICollection<CollegeLicense> CollegeLicenses ,

        public ICollection<CollegePractice> CollegePractices ,
    }
}
