using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccessLimitsAndConditionsClausesXref")]
    public class TermsOfAccessLimitsAndConditionsClause
    {
        [Required]
        public int TermsOfAccessId;

        [JsonIgnore]
        public TermsOfAccess TermsOfAccess;

        [Required]
        public int LimitsConditionsClauseId;

        [JsonIgnore]
        public LimitsAndConditionsClause LimitsConditionsClause;
    }
}
