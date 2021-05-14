using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Models
{
    [Table("Submission")]
    public class Submission : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public JObject ProfileSnapshot { get; set; }

        public AgreementType? AgreementType { get; set; }

        public bool RequestedRemoteAccess { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool Confirmed { get; set; }
    }
}
