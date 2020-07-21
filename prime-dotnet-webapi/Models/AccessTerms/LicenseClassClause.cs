using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LicenseClassClause")]
    public class LicenseClassClause : BaseAuditable, IAccessClause
    {
        public static readonly string Dispense = "Dispense";
        public static readonly string Prescribe = "Prescribe";

        public LicenseClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            AccessTermLicenseClassClauses = new List<AccessTermLicenseClassClause>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Clause { get; set; }

        public string Type { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        [JsonIgnore]
        public List<AccessTermLicenseClassClause> AccessTermLicenseClassClauses { get; set; }

        [JsonIgnore]
        public ICollection<LicenseClassClauseMapping> LicenseClassClauseMappings { get; set; }
    }
}
