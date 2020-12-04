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
                    new Facility { Code = FacilityCode.AcuteAmbulatoryCare, Name = "Acute/ambulatory care", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Facility { Code = FacilityCode.LongTermCare, Name = "Long-term care", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Facility { Code = FacilityCode.InPatientPharmacy, Name = "In-patient pharmacy", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Facility { Code = FacilityCode.OutPatientPharmacy, Name = "Out-patient pharmacy", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new Facility { Code = FacilityCode.OutPatientCommunityClinic, Name = "Outpatient or community-based clinic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
