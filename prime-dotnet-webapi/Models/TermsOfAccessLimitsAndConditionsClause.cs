using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccessLimitsAndConditionsClause")]
    public class TermsOfAccessLimitsAndConditionsClause
    {
        [Required]
        public int TermsOfAccessId { get; set; }

        [JsonIgnore]
        public TermsOfAccess TermsOfAccess { get; set; }

        [Required]
        public int LimitsConditionsClauseId { get; set; }

        [JsonIgnore]
        public LimitsAndConditionsClause LimitsAndConditionsClause { get; set; }
    }
}
