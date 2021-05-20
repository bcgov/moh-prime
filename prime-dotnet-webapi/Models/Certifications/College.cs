using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("CollegeLookup")]
    public class College : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        public ICollection<CollegePractice> CollegePractices { get; set; }

        public static bool IsCollegeOfPharmacists(int collegeCode)
        {
            return collegeCode == 2;
        }
    }
}
