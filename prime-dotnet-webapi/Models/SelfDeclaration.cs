using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SelfDeclaration")]
    public class SelfDeclaration : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public int SelfDeclarationTypeCode { get; set; }

        public SelfDeclarationType SelfDeclarationType { get; set; }

        public string SelfDeclarationDetails { get; set; }

    }
}
