using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AdjudicatorNote")]
    public class AdjudicatorNote : BaseAuditable, IEnrolleeNote
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTimeOffset NoteDate { get; set; }

        [JsonIgnore]
        public EnrolmentStatusReference EnrolmentStatusReference { get; set; }
    }
}
