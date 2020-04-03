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

        public int GlobalClauseId { get; set; }

        [JsonIgnore]
        public GlobalClause GlobalClause { get; set; }

        public int UserClauseId { get; set; }

        [JsonIgnore]
        public UserClause UserClause { get; set; }

        // TODO no longer needed in the front-end
        [NotMapped]
        public List<LicenseClassClause> LicenseClassClauses { get; set; }

        [JsonIgnore]
        public List<AccessTermLicenseClassClause> AccessTermLicenseClassClauses { get; set; }

        public int? LimitsConditionsClauseId { get; set; }

        [JsonIgnore]
        public LimitsConditionsClause LimitsConditionsClause { get; set; }

        [NotMapped]
        public string TermsOfAccess
        {
            get
            {
                return this.UserClause.Clause
                    .Replace("{$lcPlaceholder}", $"<li><p class=\"bold underline\">Additional Limits and Conditions</p><p>{this.LimitsConditionsClause.Clause}</p></li>");
            }
        }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? AcceptedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
