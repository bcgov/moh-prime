using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Agreement")]
    public class Agreement : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public int PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        [JsonIgnore]
        public int AgreementVersionId { get; set; }

        [JsonIgnore]
        public AgreementVersion AgreementVersion { get; set; }

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
