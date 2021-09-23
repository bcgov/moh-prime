using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("EnrolleeLinkedEnrolments")]
    public class EnrolleeLinkedEnrolments : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee PaperEnrollee { get; set; }
        public int PaperEnrolleeId { get; set; }

        public string UserProvidedGpid { get; set; }

        public DateTime EnrolmentCreationDate { get; set; }
    }
}
