using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class LicenseConfiguration : SeededTable<License>
    {
        public override IEnumerable<License> SeedData
        {
            get
            {
                return new[] {
                    // CPSBC
                    new License { Code = 1,  Weight = 1,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 2,  Weight = 2,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 5,  Weight = 3,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Provisional - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 6,  Weight = 4,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Provisional - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 9,  Weight = 5,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Practice Setting", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 8,  Weight = 6,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Practice Limitations", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 10, Weight = 7,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Disciplined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 22, Weight = 8,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Surgical Assistant", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 16, Weight = 9,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Clinical Observership", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 7,  Weight = 10, Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Academic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 4,  Weight = 11, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Osteopathic", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 3,  Weight = 12, Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Special", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 17, Weight = 13, Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Visitor", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 12, Weight = 14, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Resident", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 13, Weight = 15, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Resident Elective", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 14, Weight = 16, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Fellow", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 15, Weight = 17, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Trainee", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 11, Weight = 18, Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Medical Student", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 23, Weight = 19, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Administrative", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 20, Weight = 20, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Retired - Life ", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 24, Weight = 21, Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Assessment", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 18, Weight = 22, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Emergency - Family", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 19, Weight = 23, Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Emergency - Specialty", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 21, Weight = 24, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Temporarily Inactive", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 59, Weight = 25, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Podiatrists", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 65, Weight = 26, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Podiatric Surgeon Student (Elective)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 66, Weight = 27, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Podiatric Surgeon Resident (Elective)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 67, Weight = 28, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Conditional - Podiatric Surgeon Disciplined", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Pharmacy
                    new License { Code = 25, Weight = 1,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 26, Weight = 2,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Limited Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 28, Weight = 3,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Student Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 27, Weight = 4,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Temporary Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 30, Weight = 5,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = false, Name = "Non-Practicing Pharmacist", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 29, Weight = 6,  Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 31, Weight = 7,  Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 68, Weight = 8,  Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Pharmacy Technician", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // Nursing
                    new License { Code = 47, Weight = 1,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 48, Weight = 2,  Manual = true,  Validate = false, NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Provisional Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 50, Weight = 3,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Temporary Nurse Practitioner (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 51, Weight = 4,  Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Temporary Nurse Practitioner (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 49, Weight = 5,  Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = false, Name = "Non-Practicing Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 32, Weight = 6,  Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 33, Weight = 7,  Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Provisional Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 38, Weight = 8,  Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Registered Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 39, Weight = 9,  Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Registered Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 34, Weight = 10, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Registered Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 40, Weight = 11, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Employed Student Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 35, Weight = 12, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 36, Weight = 13, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Provisional Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 37, Weight = 14, Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Licensed Graduate Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 41, Weight = 15, Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 42, Weight = 16, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Provisional Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 44, Weight = 17, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Registered Psychiatric Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 45, Weight = 18, Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Registered Psychiatric Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 43, Weight = 19, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Registered Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 46, Weight = 20, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Employed Student Psychiatric Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 52, Weight = 21, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 53, Weight = 22, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Provisional Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 55, Weight = 23, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Licensed Practical Nurse (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 56, Weight = 24, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Licensed Practical Nurse (Special Event)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 54, Weight = 25, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Licensed Practical Nurse", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 57, Weight = 26, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Non-Practicing Licensed Nurse Practitioner", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 58, Weight = 27, Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Nurse Practitioner (time-limited)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 60, Weight = 28, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Practising Midwife", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 61, Weight = 29, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Provisional Midwife", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 62, Weight = 30, Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Midwife (Emergency)", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new License { Code = 63, Weight = 31, Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Non-Practising Midwife", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // All other colleges are assigned the "Not Displayed"
                    new License { Code = 64, Weight = 1,  Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Not Displayed", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}
