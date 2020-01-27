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
                    new License { Code = 1, Manual = false, Name = "Full - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 2, Manual = false, Name = "Full - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 3, Manual = true, Name = "Special", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 4, Manual = false, Name = "Osteopathic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 5, Manual = false, Name = "Provisional - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 6, Manual = false, Name = "Provisional - Speciality", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 7, Manual = true, Name = "Academic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 8, Manual = true, Name = "Conditional - Practice Limitations", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 9, Manual = true, Name = "Conditional - Practice Setting", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 10, Manual = true, Name = "Conditional - Disciplined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 11, Manual = false, Name = "Educational - Medical Student", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 12, Manual = false, Name = "Educational - Postgraduate Resident", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 13, Manual = false, Name = "Educational - Postgraduate Resident Elective", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 14, Manual = false, Name = "Educational - Postgraduate Fellow", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 15, Manual = false, Name = "Educational - Postgraduate Trainee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 16, Manual = false, Name = "Clinical Observership", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 17, Manual = false, Name = "Visitor", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 18, Manual = false, Name = "Emergency - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 19, Manual = false, Name = "Emergency - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 20, Manual = true, Name = "Retired - Life ", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 21, Manual = true, Name = "Temporarily Inactive", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 22, Manual = false, Name = "Surgical Assistant", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 23, Manual = true, Name = "Administrative", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 24, Manual = true, Name = "Assessment", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 25, Manual = false, Name = "Full Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 26, Manual = false, Name = "Limited Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 27, Manual = false, Name = "Temporary Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 28, Manual = false, Name = "Student Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 29, Manual = false, Name = "Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 30, Manual = true, Name = "Non-Practicing Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 31, Manual = true, Name = "Non-Practicing Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 32, Manual = false, Name = "Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 33, Manual = false, Name = "Provisional Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 34, Manual = false, Name = "Non-Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 35, Manual = false, Name = "Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 36, Manual = false, Name = "Provisional Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 37, Manual = false, Name = "Non-Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 38, Manual = true, Name = "Temporary Registered Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 39, Manual = false, Name = "Temporary Registered Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 40, Manual = false, Name = "Employed Student Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 41, Manual = false, Name = "Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 42, Manual = false, Name = "Provisional Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 43, Manual = true, Name = "Non-Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 44, Manual = true, Name = "Temporary Registered Psychiatric Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 45, Manual = false, Name = "Temporary Registered Psychiatric Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 46, Manual = false, Name = "Employed Student Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 47, Manual = false, Name = "Practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 48, Manual = false, Name = "Provisional Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 49, Manual = true, Name = "Non-practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 50, Manual = true, Name = "Temporary Nurse Practitioner (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 51, Manual = false, Name = "Temporary Nurse Practitioner (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 52, Manual = false, Name = "Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 53, Manual = false, Name = "Provisional Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 54, Manual = true, Name = "Non-Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 55, Manual = false, Name = "Temporary Licensed Practical Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 56, Manual = true, Name = "Temporary Licensed Practical Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 57, Manual = true, Name = "Non-practicing Licensed Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 58, Manual = true, Name = "Temporary Nurse Practitioner (time-limited)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
