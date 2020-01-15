using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    public enum AddressType
    {
        Physical = 1,
        Mailing = 2
    };

    [Table("Address")]
    public abstract class Address : IDefinable
    {
        [Key]
        public int? Id ,

        [JsonIgnore]
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public string CountryCode ,

        [JsonIgnore]
        public Country Country ,

        public string ProvinceCode ,

        [JsonIgnore]
        public Province Province ,

        public string Street ,

        public string Street2 ,

        public string City ,

        public string Postal ,
    }

    public class PhysicalAddress : Address
    { }

    public class MailingAddress : Address
    { }
}
