using FactoryGirlCore;
using system;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("TermsOfAccess")]
    public class TermsOfAccess : IDefinable
    {
        public TermsOfAccess()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }

        [Key]
        public int Id ,

        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public int GlobalClauseId ,

        public GlobalClause GlobalClause ,

        public int UserClauseId ,

        public UserClause UserClause ,

        [NotMapped]
        // TODO use the get instead of using the service to populate
        public List<LicenseClassClause> LicenseClassClauses ,

        [NotMapped]
        // TODO use the get instead of using the service to populate
        public List<LimitsAndConditionsClause> LimitsAndConditionsClauses ,

        [JsonIgnore]
        public List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses ,

        [JsonIgnore]
        public List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses ,

        [Required]
        public DateTime EffectiveDate ,
    }
}
