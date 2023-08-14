using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeDeviceProvider")]
    public class EnrolleeDeviceProvider : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        public string DeviceProviderId { get; set; }

        public string CertificationNumber { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public DeviceProviderRoleCode DeviceProviderRoleCode { get; set; }

        [JsonIgnore]
        public DeviceProviderRole DeviceProviderRole { get; set; }
    }
}
