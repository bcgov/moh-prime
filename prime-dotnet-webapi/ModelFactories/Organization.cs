using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("Organization")]
    public class Organization : IDefinable, IEnrolleeNavigationProperty
    {
        [Key]
        public int? Id ,

        [JsonIgnore]
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        [Required]
        public short OrganizationTypeCode ,

        [JsonIgnore]
        public OrganizationType OrganizationType ,
    }
}
