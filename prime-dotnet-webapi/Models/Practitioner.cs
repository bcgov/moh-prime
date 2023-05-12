using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    /// <summary>store unautorized access college id and pracRefId from transaction log then fill the first name and last name from PharmaNetAPI.</summary>
    [Table("Practitioner")]
    public class Practitioner : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string CollegeId { get; set; }

        public string PracRefId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime ProcessedDate { get; set; }
    }
}
