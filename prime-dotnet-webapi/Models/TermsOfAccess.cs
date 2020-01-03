using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class TermsOfAccess: BaseAuditable
    {
        public TermsOfAccess()
        {
            // Create lists so they don't have be instantiated when items need to be added
            LicenceClassClauses = new List<LicenceClassClause>();
            LimitsConditionsClauses = new List<LimitsAndConditionsClause>();
        }

        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public GlobalClause GlobalClauseId { get; set; }

        public GlobalClause GlobalClause { get; set; }

        public UserClause UserClauseId { get; set; }

        public UserClause UserClause { get; set; }

        public ICollection<LicenceClassClause> LicenceClassClauses { get; set; }

        public ICollection<LimitsAndConditionsClause> LimitsConditionsClauses { get; set; }
    }
}
