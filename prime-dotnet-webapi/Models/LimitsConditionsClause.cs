using System;
using System.Collections.Generic;
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

        public Enrollee Enrollee { get; set; }

        [Required]
        public string Clause { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

    }
}
