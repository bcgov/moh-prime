using System;
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

        public GlobalClause GlobalClause { get; set; }

        public int UserClauseId { get; set; }

        public UserClause UserClause { get; set; }
        [NotMapped]
        public List<LicenseClassClause> LicenseClassClauses { get; set; }

        [JsonIgnore]
        public List<AccessTermLicenseClassClause> AccessTermLicenseClassClauses { get; set; }

        public int? LimitsConditionsClauseId { get; set; }

        public LimitsConditionsClause LimitsConditionsClause { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime AcceptedDate { get; set; }
    }
}
