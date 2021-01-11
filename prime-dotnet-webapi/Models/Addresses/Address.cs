using System;
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
        public int Id { get; set; }

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

        [NotMapped]
        public bool IsInBC
        {
            get => Province.BRITISH_COLUMBIA_CODE.Equals(ProvinceCode, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Updates this Address with the values from another address, if not null.
        /// </summary>
        /// <param name="other"></param>
        public void SetValues(Address other)
        {
            if (other == null)
            {
                return;
            }

            CountryCode = other.CountryCode;
            ProvinceCode = other.ProvinceCode;
            Street = other.Street;
            Street2 = other.Street2;
            City = other.City;
            Postal = other.Postal;
        }
    }
    public class PhysicalAddress : Address
    { }

    public class MailingAddress : Address
    { }
}
