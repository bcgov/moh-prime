using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AccessTerm")]
    public class AccessTerm : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [JsonIgnore]
        public int UserClauseId { get; set; }

        [JsonIgnore]
        public UserClause UserClause { get; set; }

        [JsonIgnore]
        public int? LimitsConditionsClauseId { get; set; }

        [JsonIgnore]
        public LimitsConditionsClause LimitsConditionsClause { get; set; }

        [NotMapped]
        public string TermsOfAccess { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? AcceptedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
