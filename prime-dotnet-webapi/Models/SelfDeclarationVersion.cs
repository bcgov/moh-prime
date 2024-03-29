using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("SelfDeclarationVersion")]
    public class SelfDeclarationVersion : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SelfDeclarationTypeCode { get; set; }

        public SelfDeclarationType SelfDeclarationType { get; set; }

        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        // A comma-separated list of all care setting codes for which applicable
        public string CareSettingCodeStr { get; set; }
    }
}
