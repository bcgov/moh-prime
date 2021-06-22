using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class FacilityConfiguration : SeededTable<Facility>
    {
        public override IEnumerable<Facility> SeedData
        {
            get
            {
                return new[] {
                    new Facility { Code = FacilityCode.AcuteAmbulatoryCare,       Name = "Acute/ambulatory care"                },
                    new Facility { Code = FacilityCode.LongTermCare,              Name = "Long-term care"                       },
                    new Facility { Code = FacilityCode.InPatientPharmacy,         Name = "In-patient pharmacy"                  },
                    new Facility { Code = FacilityCode.OutPatientPharmacy,        Name = "Out-patient pharmacy"                 },
                    new Facility { Code = FacilityCode.OutPatientCommunityClinic, Name = "Outpatient or community-based clinic" }
                };
            }
        }
    }
}
