using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.ModelFactories
{

    [Table("Certification")]
    public class Certification : IDefinable, IEnrolleeNavigationProperty
    {
        [Key]
        public int? Id ,

        [JsonIgnore]
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        [Required]
        public short CollegeCode ,

        [JsonIgnore]
        public College College ,

        [Required]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "License Number should not contain characters")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "License Number must be 5 digits")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string LicenseNumber ,

        [Required]
        public short LicenseCode ,

        [JsonIgnore]
        public License License ,

        [Required]
        public DateTime RenewalDate ,

        public short? PracticeCode ,

        [JsonIgnore]
        public Practice Practice ,

        [NotMapped]
        [JsonIgnore]
        public string FullLicenseNumber { get { return $"{College?.Prefix}-{LicenseNumber}"; } }
    }
}
