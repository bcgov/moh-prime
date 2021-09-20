using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.Configuration
{
    public class SecurityGroupConfiguration : SeededTable<SecurityGroup>
    {
        public override IEnumerable<SecurityGroup> SeedData
        {
            get
            {
                return new[] {
                    new SecurityGroup { Code = SecurityGroupCode.EmrCommunityBasedClinics,  Name = "EMRMD (EMR - Community-based Clinics)" },
                    new SecurityGroup { Code = SecurityGroupCode.HospitalAdmitting,  Name = "HAD (Hospital Admitting)" },
                    new SecurityGroup { Code = SecurityGroupCode.HealthAuthorityViewer,  Name = "HAI (HA Viewer)" },
                    new SecurityGroup { Code = SecurityGroupCode.HospitalAccess,  Name = "HAP (Hospital Access)" },
                    new SecurityGroup { Code = SecurityGroupCode.EmergencyDepartmentAccess,  Name = "HNF (Emergency Department Access (EDAP))" },
                    new SecurityGroup { Code = SecurityGroupCode.InpatientPharmaciesHospital,  Name = "IP (In-patient Pharmacies - Hospital)" },
                    new SecurityGroup { Code = SecurityGroupCode.Compap,  Name = "MD (COMPAP)" },
                    new SecurityGroup { Code = SecurityGroupCode.HospitalOutpatientPharmacy,  Name = "OP (Hospital Outpatient Pharmacy)" },
                    new SecurityGroup { Code = SecurityGroupCode.CernerIntegrationSite,  Name = "VHA (Cerner Integration Site)" }
                };
            }
        }
    }
}
