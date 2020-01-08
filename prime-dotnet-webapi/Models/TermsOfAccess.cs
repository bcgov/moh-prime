using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("TermsOfAccess")]
    public class TermsOfAccess : BaseAuditable
    {
        public TermsOfAccess()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }

        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int GlobalClauseId { get; set; }

        public GlobalClause GlobalClause { get; set; }

        public int UserClauseId { get; set; }

        public UserClause UserClause { get; set; }

        [NotMapped]
        // TODO use the get instead of using the service to populate
        public List<LicenseClassClause> LicenseClassClauses { get; set; }

        [NotMapped]
        // TODO use the get instead of using the service to populate
        public List<LimitsAndConditionsClause> LimitsAndConditionsClauses { get; set; }

        [JsonIgnore]
        public List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses { get; set; }

        [JsonIgnore]
        public List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }
    }
}
