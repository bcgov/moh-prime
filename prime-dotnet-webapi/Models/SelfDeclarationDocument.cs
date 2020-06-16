using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SelfDeclarationDocument")]
    public class SelfDeclarationDocument : BaseDocumentUpload
    {

        [Required]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public int SelfDeclarationTypeCode { get; set; }

        public SelfDeclarationType SelfDeclarationType { get; set; }

    }
}
