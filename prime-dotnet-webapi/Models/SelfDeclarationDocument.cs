using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SelfDeclarationDocument")]
    public class SelfDeclarationDocument : BaseDocumentUpload
    {

        public int SelfDeclarationId { get; set; }

        [JsonIgnore]
        public SelfDeclaration SelfDeclaration { get; set; }

    }
}
