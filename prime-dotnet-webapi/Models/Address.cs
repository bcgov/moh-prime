using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    public enum AddressType
    {
        Physical = 1,
        Mailing = 2
    };

    [Table("Address")]
    public abstract class Address : BaseAuditable
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public string CountryCode { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        public string ProvinceCode { get; set; }

        [JsonIgnore]
        public Province Province { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Postal { get; set; }
    }

    public class PhysicalAddress : Address
    { }

    public class MailingAddress : Address
    { }
}
