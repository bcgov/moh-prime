using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LimitsAndConditionsClause")]
    public class LimitsAndConditionsClause : BaseAuditable, IAccessClause
    {
        public LimitsAndConditionsClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [JsonIgnore]
        public List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses { get; set; }
    }
}
