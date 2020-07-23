using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Party")]
    public class Party : BaseAuditable, IUserBoundModel
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(255)]
        public string HPDID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string JobRoleTitle { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string SMSPhone { get; set; }
    }
}
