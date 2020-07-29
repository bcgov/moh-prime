using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Prime.Models
{
    [Table("LimitsConditionsClause")]
    public class LimitsConditionsClause : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }
    }
}
