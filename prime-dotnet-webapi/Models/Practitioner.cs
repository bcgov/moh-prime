using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    /// <summary>store college id, pracRefId and the contact information.
    /// mainly used by unauthorized reporting from transaction log.</summary>
    [Table("Practitioner")]
    public class Practitioner : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public string CollegeId { get; set; }
        public string PracRefId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? EffectiveDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}
