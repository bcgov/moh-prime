using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class DeviceProviderRoleConfiguration : SeededTable<DeviceProviderRole>
    {
        public override IEnumerable<DeviceProviderRole> SeedData
        {
            get
            {
                return new[] {

                    new DeviceProviderRole { Code = DeviceProviderRoleCode.CertifiedProsthetist,  Weight = 1, Name = "Certified Prosthetist", Certified = true },
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.CertifiedOrthotist,  Weight = 2, Name = "Certified Orthotist", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.CertifiedProsthetistOrthotist,  Weight = 3, Name = "Certified Prosthetist Orthotist", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.RegisteredProstheticTechnician,  Weight = 4, Name = "Registered Prosthetic Technician", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.RegisteredOrthoticTechnician,  Weight = 5, Name = "Registered Orthotic Technician", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.RegisteredProstheticOrthoticTechnician,  Weight = 6, Name = "Registered Prosthetic Orthotic Technician", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.OrthoticResident, Weight = 7, Name = "Orthotic Resident", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.ProstheticResident, Weight = 8, Name = "Prosthetic Resident", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.OrthoticIntern, Weight = 9, Name = "Orthotic Intern", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.ProstheticIntern, Weight = 10, Name = "Prosthetic Intern", Certified = true},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.CompressionGarmentFitter, Weight = 11, Name = "Compression Garment Fitter", Certified = false},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.BreastProstheticFitter, Weight = 12, Name = "Breast prosthetic Fitter", Certified = false},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.Ocularist, Weight = 13, Name = "Ocularist", Certified = false},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.Anaplastologist, Weight = 14, Name = "Anaplastologist", Certified = false},
                    new DeviceProviderRole { Code = DeviceProviderRoleCode.None, Weight = 15, Name = "None", Certified = false},
                };
            }
        }
    }
}
