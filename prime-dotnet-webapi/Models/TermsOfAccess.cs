using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    // TODO: TermsOfAccess or AccessTerm so table name can be plural
    public class TermsOfAccess: BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public GlobalClause GlobalClauseId { get; set; }

        [JsonIgnore]
        public GlobalClause GlobalClause { get; set; }

        public UserClause UserClauseId { get; set; }

        [JsonIgnore]
        public UserClause UserClause { get; set; }

        public ICollection<LicenceClassClause> LicenceClassClauses { get; set; }

        public ICollection<LimitsAndConditionsClause> LimitsConditionsClauses { get; set; }
    }
}
