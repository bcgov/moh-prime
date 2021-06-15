
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    /// <summary>
    /// This is related to PLR-sourced data
    /// </summary>
    [Table("IdentifierTypeLookup")]
    public class IdentifierType : ILookup<string>
    {
        /// <summary>
        /// Identifier OID, e.g. 2.16.840.1.113883.3.40.2.20
        /// </summary>
        [Key]
        public string Code { get; set; }

        /// <summary>
        /// Concept referenced by OID, e.g. RNPID aka "British Columbia Registered Nurse Practitioner ID"
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
