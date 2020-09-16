using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("LimitsConditionsClause")]
    public class LimitsConditionsClause : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        public static LimitsConditionsClause FromAgreementNote(AccessAgreementNote agreementNote)
        {
            string text = agreementNote?.Note;

            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            return new LimitsConditionsClause
            {
                Text = text,
                EffectiveDate = DateTimeOffset.Now
            };
        }
    }
}
