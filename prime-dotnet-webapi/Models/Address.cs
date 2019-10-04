using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    public enum AddressType : int
    {
        Physical = 1,
        Mailing = 2
    };

    [Table("Address")]
    public abstract class Address
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postal { get; set; }
    }

    public class PhysicalAddress : Address
    { }

    public class MailingAddress : Address
    { }
}