using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PracticeLookup")]
    public class Practice : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegePractice> CollegePractices { get; set; }
    }
}
