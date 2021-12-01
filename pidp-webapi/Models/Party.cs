using NodaTime;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pidp.Models
{
    [Table(nameof(Party))]
    public class Party
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public LocalDate DateOfBirth { get; set; }

        // [Required]
        public PartyAddress? PhysicalAddress { get; set; }
    }
}
