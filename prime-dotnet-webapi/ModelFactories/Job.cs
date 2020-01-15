using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("Job")]
    public class Job : IDefinable, IEnrolleeNavigationProperty
    {
        [Key]
        public int? Id ,

        [JsonIgnore]
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        [Required]
        public string Title ,
    }
}
