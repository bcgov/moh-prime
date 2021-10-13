using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("AgreementVersion")]
    public class AgreementVersion : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public AgreementType AgreementType { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }
    }
}
