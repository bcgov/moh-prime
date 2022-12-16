using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SelfDeclarationTypeLookup")]
    public class SelfDeclarationType
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        [JsonIgnore]
        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
    }
}
