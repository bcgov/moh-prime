using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    /// <summary>
    /// used for device provider
    /// </summary>
    [Table("DeviceProviderRoleLookup")]
    public class DeviceProviderRole : ILookup<DeviceProviderRoleCode>
    {
        [Key]
        public DeviceProviderRoleCode Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int Weight { get; set; }

        [Required]
        public bool Certified { get; set; }
    }
}
