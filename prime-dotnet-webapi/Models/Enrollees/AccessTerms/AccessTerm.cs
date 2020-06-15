using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AccessTerm")]
    public class AccessTerm : BaseAuditable
    {
        public AccessTerm()
        {
            // Create lists so they don't have be instantiated when items need to be added
            AccessTermLicenseClassClauses = new List<AccessTermLicenseClassClause>();
        }

        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [JsonIgnore]
        public int GlobalClauseId { get; set; }

        [JsonIgnore]
        public GlobalClause GlobalClause { get; set; }

        [JsonIgnore]
        public int UserClauseId { get; set; }

        [JsonIgnore]
        public UserClause UserClause { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<LicenseClassClause> LicenseClassClauses { get; set; }

        [JsonIgnore]
        public List<AccessTermLicenseClassClause> AccessTermLicenseClassClauses { get; set; }

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
