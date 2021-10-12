using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
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
