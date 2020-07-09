using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SelfDeclarationDocument")]
    public class SelfDeclarationDocument : BaseDocumentUpload, IEnrolleeNavigationProperty
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int SelfDeclarationTypeCode { get; set; }

        [JsonIgnore]
        public SelfDeclarationType SelfDeclarationType { get; set; }
    }
}
