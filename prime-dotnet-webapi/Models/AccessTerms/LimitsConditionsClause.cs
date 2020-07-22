using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;

namespace Prime.Models
{
    [Table("LimitsConditionsClause")]
    public class LimitsConditionsClause : BaseAuditable, IAccessClause
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public string Clause { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }
    }
}
