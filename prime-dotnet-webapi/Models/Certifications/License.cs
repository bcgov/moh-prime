using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DelegateDecompiler;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseLookup")]
    public class License : ILookup<int>
    {
        public License()
        {
            // Initialize collections to prevent null exception on computed properties
            LicenseDetails = new List<LicenseDetail>();
        }

        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int Weight { get; set; }

        [JsonIgnore]
        public ICollection<LicenseDetail> LicenseDetails { get; set; }

        [JsonIgnore]
        public ICollection<Certification> Certifications { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        public ICollection<RemoteAccessTypeLicense> RemoteAccessTypeLicenses { get; set; }

        [JsonIgnore]
        public ICollection<DefaultPrivilege> DefaultPrivileges { get; set; }

        [Computed]
        [NotMapped]
        public LicenseDetail CurrentLicenseDetail
        {
            get => LicenseDetails
                .Where(l => l.EffectiveDate <= DateTime.UtcNow)
                .OrderByDescending(s => s.EffectiveDate)
                .FirstOrDefault();
        }

        public static bool IsPharmacyTechnician(LicenseDetail licenseDetail)
        {
            return licenseDetail.Prefix == "T9";
        }

        public static bool isLicensedPracticalNurse(LicenseDetail licenseDetail)
        {
            return licenseDetail.Prefix == "L9";
        }

        public static bool isPhysicianAssistant(LicenseDetail licenseDetail)
        {
            return licenseDetail.Prefix == "M9";
        }
    }
}
