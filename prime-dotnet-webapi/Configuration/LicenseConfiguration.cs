using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class LicenseConfiguration : SeededTable<License>
    {
        public override ICollection<License> SeedData
        {
            get
            {
                return new[] {
                    new License { Code = 1, Name = "Full - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 2, Name = "Full - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 3, Name = "Special", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 4, Name = "Osteopathic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 5, Name = "Provisional - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 6, Name = "Provisional - Speciality", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 7, Name = "Academic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 8, Name = "Conditional - Practice Limitations", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 9, Name = "Conditional - Practice Setting", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 10, Name = "Conditional - Disciplined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 11, Name = "Educational - Medical Student", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 12, Name = "Educational - Postgraduate Resident", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 13, Name = "Educational - Postgraduate Resident Elective", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 14, Name = "Educational - Postgraduate Fellow", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 15, Name = "Educational - Postgraduate Trainee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 16, Name = "Clinical Observership", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 17, Name = "Visitor", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 18, Name = "Emergency - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 19, Name = "Emergency - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 20, Name = "Retired - Life ", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 21, Name = "Temporarily Inactive", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 22, Name = "Surgical Assistant", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 23, Name = "Administrative", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 24, Name = "Assessment", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 25, Name = "Full Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 26, Name = "Limited Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 27, Name = "Temporary Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 28, Name = "Student Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 29, Name = "Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 30, Name = "Non-Practicing Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 31, Name = "Non-Practicing Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 32, Name = "Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 33, Name = "Provisional Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 34, Name = "Non-Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 35, Name = "Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 36, Name = "Provisional Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 37, Name = "Non-Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 38, Name = "Temporary Registered Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 39, Name = "Temporary Registered Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 40, Name = "Employed Student Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 41, Name = "Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 42, Name = "Provisional Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 43, Name = "Non-Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 44, Name = "Temporary Registered Psychiatric Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 45, Name = "Temporary Registered Psychiatric Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 46, Name = "Employed Student Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 47, Name = "Practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 48, Name = "Provisional Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 49, Name = "Non-practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 50, Name = "Temporary Nurse Practitioner (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 51, Name = "Temporary Nurse Practitioner (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 52, Name = "Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 53, Name = "Provisional Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 54, Name = "Non-Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 55, Name = "Temporary Licensed Practical Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 56, Name = "Temporary Licensed Practical Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
