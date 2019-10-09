using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
