using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.ModelFactories
{
    [Table("LimitsAndConditionsClause")]
    public class LimitsAndConditionsClause : IDefinable, IAccessClause
    {
        public LimitsAndConditionsClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }

        [Key]
        public int Id ,

        [Required]
        public string Clause ,

        [Required]
        public DateTime EffectiveDate ,

        [JsonIgnore]
        public List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses ,
    }
}
