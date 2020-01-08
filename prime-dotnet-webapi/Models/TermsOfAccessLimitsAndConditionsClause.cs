using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccessLimitsAndConditionsClauseXref")]
    public class TermsOfAccessLimitsAndConditionsClause
    {
        [Required]
        public int TermsOfAccessId { get; set; }

        public TermsOfAccess TermsOfAccess { get; set; }

        [Required]
        public int LimitsConditionsClauseId { get; set; }

        public LimitsAndConditionsClause LimitsAndConditionsClause { get; set; }
    }
}
