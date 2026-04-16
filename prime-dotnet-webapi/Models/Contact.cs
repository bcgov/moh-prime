using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Contact")]
    public class Contact : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string JobRoleTitle { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string Fax { get; set; }

        public string SMSPhone { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }
    }
}
