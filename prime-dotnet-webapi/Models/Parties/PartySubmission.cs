using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PartySubmission")]
    public class PartySubmission : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        public SubmissionType SubmissionType { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool Approved { get; set; }
    }
}
