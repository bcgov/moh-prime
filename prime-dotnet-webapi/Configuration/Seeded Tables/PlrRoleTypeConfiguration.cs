using System.Collections.Generic;
using Prime.Models.Plr;

namespace Prime.Configuration
{
    public class PlrRoleTypeConfiguration : SeededTable<PlrRoleType>
    {
        public override IEnumerable<PlrRoleType> SeedData
        {
            get
            {
                return new[] {
                    new PlrRoleType { Code = "RTNM",      Name = "Nuclear Medicine Technologist" },
                    new PlrRoleType { Code = "RTR",       Name = "Radiation Technologist in Radiology" },
                    new PlrRoleType { Code = "RTT",       Name = "Radiation Technologist in Therapy" },
                    new PlrRoleType { Code = "RN",        Name = "Registered Nurse" },
                    new PlrRoleType { Code = "RNP",       Name = "Registered Nurse Practitioner" },
                    new PlrRoleType { Code = "RPN",       Name = "Registered Psychiatric Nurse" },
                    new PlrRoleType { Code = "RTEMG",     Name = "Registered Electromyography Technologist" },
                    new PlrRoleType { Code = "RTMR",      Name = "Radiation Technologist in Magnetic Resonance" },
                    new PlrRoleType { Code = "PHARM",     Name = "Pharmacist" },
                    new PlrRoleType { Code = "PO",        Name = "Podiatrist" },
                    new PlrRoleType { Code = "RAC",       Name = "Registered Acupuncturist" },
                    new PlrRoleType { Code = "REPT",      Name = "Registered Evoked Potential Technologist" },
                    new PlrRoleType { Code = "RET",       Name = "Registered Electroencephalography Technologist" },
                    new PlrRoleType { Code = "RM",        Name = "Registered Midwife" },
                    new PlrRoleType { Code = "LPN",       Name = "Licensed Practical Nurse" },
                    new PlrRoleType { Code = "MD",        Name = "Medical Doctor" },
                    new PlrRoleType { Code = "MOA",       Name = "Medical Office Assistant" },
                    new PlrRoleType { Code = "OPT",       Name = "Optometrist" },
                    new PlrRoleType { Code = "PCP",       Name = "Primary Care Paramedic" },
                    new PlrRoleType { Code = "PCY",       Name = "Pharmacy" },
                    new PlrRoleType { Code = "ACP",       Name = "Advanced Care Paramedic" },
                    new PlrRoleType { Code = "CCP",       Name = "Critical Care Paramedic" },
                    new PlrRoleType { Code = "DEN",       Name = "Dentist" },
                    new PlrRoleType { Code = "EMR",       Name = "Emergency Medical Responder" },
                    new PlrRoleType { Code = "CC",        Name = "Clinical Counsellor" },
                    new PlrRoleType { Code = "OT",        Name = "Occupational Therapist" },
                    new PlrRoleType { Code = "PSYCH",     Name = "Psychologist" },
                    new PlrRoleType { Code = "SW",        Name = "Social Worker" },
                    new PlrRoleType { Code = "RCSW",      Name = "Registered Clinical Social Worker" },
                    new PlrRoleType { Code = "CHIRO",     Name = "Chiropractor" },
                    new PlrRoleType { Code = "PHYSIO",    Name = "Physiotherapist" },
                    new PlrRoleType { Code = "RMT",       Name = "Registered Massage Therapist" },
                    new PlrRoleType { Code = "KN",        Name = "Kinesiologist" },
                    new PlrRoleType { Code = "PTECH",     Name = "Pharmacy Technician" },
                    new PlrRoleType { Code = "COUN",      Name = "Counsellor" },
                    new PlrRoleType { Code = "MFT",       Name = "Marriage and Family Therapist" },
                    new PlrRoleType { Code = "RD",        Name = "Registered Dietitian" },
                    new PlrRoleType { Code = "PHARMTECH", Name = "PHARMTECH" }
                };
            }
        }
    }
}
