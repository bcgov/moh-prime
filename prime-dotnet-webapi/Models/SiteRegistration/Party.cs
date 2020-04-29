using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string JobRoleTitle { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string SMSPhone { get; set; }
    }
}
