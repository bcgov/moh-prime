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

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int SelfDeclarationTypeCode { get; set; }

        public SelfDeclarationType SelfDeclarationType { get; set; }

        public string SelfDeclarationDetails { get; set; }

        // This is a holdover till we do a propper refactor of self delaration documents being created at the same time as self declarations
        [NotMapped]
        public ICollection<Guid> DocumentGuids { get; set; }
    }
}
