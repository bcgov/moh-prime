using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class LicenseConfiguration : SeededTable<License>
    {
        public override IEnumerable<License> SeedData
        {
            get
            {
                return new[] {
                    // CPSBC
                    new License { Code = 1,  Weight = 1,  Name = "Full - Family"                                       },
                    new License { Code = 2,  Weight = 2,  Name = "Full - Specialty"                                    },
                    new License { Code = 5,  Weight = 3,  Name = "Provisional - Family"                                },
                    new License { Code = 6,  Weight = 4,  Name = "Provisional - Specialty"                             },
                    new License { Code = 9,  Weight = 5,  Name = "Conditional - Practice Setting"                      },
                    new License { Code = 8,  Weight = 6,  Name = "Conditional - Practice Limitations"                  },
                    new License { Code = 10, Weight = 7,  Name = "Conditional - Disciplined"                           },
                    new License { Code = 22, Weight = 8,  Name = "Surgical Assistant"                                  },
                    new License { Code = 16, Weight = 9,  Name = "Clinical Observership"                               },
                    new License { Code = 7,  Weight = 10, Name = "Academic"                                            },
                    new License { Code = 4,  Weight = 11, Name = "Osteopathic"                                         },
                    new License { Code = 3,  Weight = 12, Name = "Special"                                             },
                    new License { Code = 17, Weight = 13, Name = "Visitor"                                             },
                    new License { Code = 12, Weight = 14, Name = "Educational - Postgraduate Resident"                 },
                    new License { Code = 179,Weight = 15, Name = "Educational - Postgraduate Resident Clinical Associate"},
                    new License { Code = 13, Weight = 15, Name = "Educational - Postgraduate Resident Elective"        },
                    new License { Code = 14, Weight = 16, Name = "Educational - Postgraduate Fellow"                   },
                    new License { Code = 15, Weight = 17, Name = "Educational - Postgraduate Trainee"                  },
                    new License { Code = 11, Weight = 18, Name = "Educational - Medical Student"                       },
                    new License { Code = 23, Weight = 19, Name = "Administrative"                                      },
                    new License { Code = 20, Weight = 20, Name = "Retired - Life"                                      },
                    new License { Code = 90, Weight = 21, Name = "Assessment - Family"                                 },
                    new License { Code = 91, Weight = 22, Name = "Assessment - Specialty"                              },
                    new License { Code = 24, Weight = 23, Name = "Assessment"                                          },
                    new License { Code = 18, Weight = 24, Name = "Emergency - Family"                                  },
                    new License { Code = 19, Weight = 25, Name = "Emergency - Specialty"                               },
                    new License { Code = 21, Weight = 26, Name = "Temporarily Inactive"                                },
                    new License { Code = 87, Weight = 27, Name = "Associate - Acute Care"                              },
                    new License { Code = 88, Weight = 28, Name = "Associate - Community Primary Care"                  },
                    new License { Code = 89, Weight = 29, Name = "USA Certified"                                       },
                    new License { Code = 92, Weight = 30, Name = "Certified Physician Assistant"                       },
                    new License { Code = 59, Weight = 31, Name = "Full - Podiatric Surgeon"                            },
                    new License { Code = 65, Weight = 32, Name = "Educational - Podiatric Surgeon Student (Elective)"  },
                    new License { Code = 66, Weight = 33, Name = "Educational - Podiatric Surgeon Resident (Elective)" },
                    new License { Code = 67, Weight = 34, Name = "Conditional - Podiatric Surgeon Disciplined"         },

                    // Pharmacy
                    new License { Code = 25, Weight = 10, Name = "Full Pharmacist"                    },
                    new License { Code = 26, Weight = 20, Name = "Limited Pharmacist"                 },
                    new License { Code = 28, Weight = 30, Name = "Student Pharmacist"                 },
                    new License { Code = 27, Weight = 40, Name = "Temporary Pharmacist"               },
                    new License { Code = 178, Weight = 45, Name = "Temporary Limited Pharmacist"      },
                    new License { Code = 30, Weight = 50, Name = "Non-Practicing Pharmacist"          },
                    new License { Code = 29, Weight = 60, Name = "Pharmacy Technician"                },
                    new License { Code = 31, Weight = 70, Name = "Non-Practicing Pharmacy Technician" },
                    new License { Code = 68, Weight = 80, Name = "Temporary Pharmacy Technician"      },

                    // Nursing
                    new License { Code = 47,  Weight = 1,  Name = "Practicing Nurse Practitioner"                          },
                    new License { Code = 48,  Weight = 2,  Name = "Provisional Nurse Practitioner"                         },
                    new License { Code = 51,  Weight = 4,  Name = "Temporary Nurse Practitioner (Emergency)"               },
                    new License { Code = 49,  Weight = 5,  Name = "Non-Practicing Nurse Practitioner"                      },
                    new License { Code = 32,  Weight = 11, Name = "Practicing Registered Nurse"                            },
                    new License { Code = 33,  Weight = 12, Name = "Provisional Registered Nurse"                           },
                    new License { Code = 39,  Weight = 13, Name = "Temporary Registered Nurse (Emergency)"                 },
                    new License { Code = 175, Weight = 14, Name = "RN – Multijurisdictional"                               },
                    new License { Code = 34,  Weight = 15, Name = "Non-Practicing Registered Nurse"                        },
                    new License { Code = 40,  Weight = 16, Name = "Employed Student Nurse"                                 },
                    new License { Code = 35,  Weight = 17, Name = "Practicing Licensed Graduate Nurse"                     },
                    new License { Code = 36,  Weight = 18, Name = "Provisional Licensed Graduate Nurse"                    },
                    new License { Code = 37,  Weight = 19, Name = "Non-Practicing Licensed Graduate Nurse"                 },
                    new License { Code = 41,  Weight = 21, Name = "Practicing Registered Psychiatric Nurse"                },
                    new License { Code = 42,  Weight = 22, Name = "Provisional Registered Psychiatric Nurse"               },
                    new License { Code = 45,  Weight = 23, Name = "Temporary Registered Psychiatric Nurse (Emergency)"     },
                    new License { Code = 176, Weight = 24, Name = "RPN – Multijurisdictional"                              },
                    new License { Code = 43,  Weight = 25, Name = "Non-Practicing Registered Psychiatric Nurse"            },
                    new License { Code = 46,  Weight = 26, Name = "Employed Student Psychiatric Nurse"                     },
                    new License { Code = 52,  Weight = 31, Name = "Practicing Licensed Practical Nurse"                    },
                    new License { Code = 53,  Weight = 32, Name = "Provisional Licensed Practical Nurse"                   },
                    new License { Code = 55,  Weight = 33, Name = "Temporary Licensed Practical Nurse (Emergency)"         },
                    new License { Code = 177, Weight = 34, Name = "LPN – Multijurisdictional"                              },
                    new License { Code = 54,  Weight = 35, Name = "Non-Practicing Licensed Practical Nurse"                },
                    new License { Code = 60,  Weight = 41, Name = "Practising Midwife"                                     },
                    new License { Code = 61,  Weight = 42, Name = "Provisional Midwife"                                    },
                    new License { Code = 62,  Weight = 43, Name = "Temporary Midwife (Emergency)"                          },
                    new License { Code = 63,  Weight = 44, Name = "Non-Practising Midwife"                                 },
                    new License { Code = 69,  Weight = 45, Name = "Student Midwife"                                        },

                    // Dental Surgeons
                    new License { Code = 70, Weight = 1, Name = "Full Registrations" },
                    new License { Code = 75, Weight = 2, Name = "Restricted to Specialty" },
                    new License { Code = 76, Weight = 3, Name = "Academic" },
                    new License { Code = 77, Weight = 4, Name = "Academic (Grand-parented)" },

                    // Naturopaths
                    new License { Code = 78, Weight = 1, Name = "Full" },
                    new License { Code = 79, Weight = 2, Name = "Non-practicing" },
                    new License { Code = 80, Weight = 3, Name = "Temporary" },
                    new License { Code = 81, Weight = 4, Name = "Student" },

                    // Optometrists
                    new License { Code = 71, Weight = 1, Name = "Therapeutic Optometrist" },
                    new License { Code = 72, Weight = 2, Name = "Non-Therapeutic Optometrist" },
                    new License { Code = 73, Weight = 3, Name = "Non-Practicing Optometrist" },
                    new License { Code = 74, Weight = 4, Name = "Limited Optometrist" },

                    //BC College of Social Workers
                    new License { Code = 82, Weight = 1, Name = "Full registration" },
                    new License { Code = 83, Weight = 2, Name = "Clinical registration" },
                    new License { Code = 84, Weight = 3, Name = "Provisional registration" },
                    new License { Code = 85, Weight = 4, Name = "Non-practicing registration" },
                    new License { Code = 86, Weight = 4, Name = "Temporary registration" },


                    //College of Oral Health Professionals
                    // Dental Assistant
                    new License { Code = 93, Weight = 120, Name = "Full Certified Dental Assisant" },
                    new License { Code = 94, Weight = 121, Name = "Limited Certified Dental Assistant" },
                    new License { Code = 95, Weight = 123, Name = "Non-Practising Certified Dental Assistant" },
                    new License { Code = 96, Weight = 124, Name = "Temporary Certified Dental Assistant" },

                    // Dental Hygienist
                    new License { Code = 97, Weight = 131, Name = "Registered Dental Hygienist" },
                    new License { Code = 98, Weight = 132, Name = "Dental Hygiene Practitioner" },
                    new License { Code = 99, Weight = 133, Name = "Non-Practising Dental Hygienist" },
                    new License { Code = 100, Weight = 134, Name = "Temporary Dental Hygienist" },

                    // Dental Technician
                    new License { Code = 101, Weight = 141, Name = "Dental Technician" },
                    new License { Code = 102, Weight = 142, Name = "Student Dental Technician" },
                    new License { Code = 103, Weight = 143, Name = "Non-Practising Dental Technician" },
                    new License { Code = 104, Weight = 144, Name = "Temporary Dental Technician" },

                    // Dental Therapist
                    new License { Code = 105, Weight = 110, Name = "Dental Therapist" },

                    // Dentist
                    new License { Code = 106, Weight = 100, Name = "Full Dentist" },
                    new License { Code = 107, Weight = 102, Name = "Limited (Academic) Dentist" },
                    new License { Code = 108, Weight = 103, Name = "Limited (Armed Services or Government) Dentist" },
                    new License { Code = 109, Weight = 104, Name = "Limited (Education & Volunteer) Dentist" },
                    new License { Code = 110, Weight = 105, Name = "Limited (Restricted-to-Specialty) Dentist" },
                    new License { Code = 111, Weight = 106, Name = "Student Dentist" },
                    new License { Code = 112, Weight = 107, Name = "Non-Practising Dentist" },
                    new License { Code = 113, Weight = 108, Name = "Temporary Dentist" },

                    // Denturist
                    new License { Code = 114, Weight = 151, Name = "Full Denturist" },
                    new License { Code = 115, Weight = 152, Name = "Limited Denturist" },
                    new License { Code = 116, Weight = 153, Name = "Limited (Grandfathered) Denturist" },
                    new License { Code = 117, Weight = 154, Name = "Student Denturist" },
                    new License { Code = 118, Weight = 155, Name = "Non-Practising Denturist" },
                    new License { Code = 119, Weight = 156, Name = "Temporary Denturist" },




                    // New license class for College of Health and Care Professionals of British Columbia
                    // Dietetics
                    new License { Code = 120, Weight = 10, Name = "Full" },
                    new License { Code = 121, Weight = 20, Name = "Emergency" },
                    new License { Code = 122, Weight = 30, Name = "Temporary" },
                    new License { Code = 123, Weight = 40, Name = "Non-Practising" },
                    // Occupational Therapy
                    new License { Code = 124, Weight = 10, Name = "Full" },
                    new License { Code = 125, Weight = 20, Name = "Provisional" },
                    new License { Code = 126, Weight = 30, Name = "Temporary" },
                    new License { Code = 127, Weight = 40, Name = "Non-Practising" },
                    // Opticianry
                    new License { Code = 128, Weight = 10, Name = "Registered Optician" },
                    new License { Code = 129, Weight = 20, Name = "Registered Contact Lens Fitter" },
                    new License { Code = 130, Weight = 30, Name = "Temporary" },
                    new License { Code = 131, Weight = 40, Name = "Non-Practising" },
                    // Optometry
                    new License { Code = 132, Weight = 10, Name = "Therapeutic Qualified" },
                    new License { Code = 133, Weight = 20, Name = "Non-Therapeutic Qualified" },
                    new License { Code = 134, Weight = 30, Name = "Limited" },
                    new License { Code = 135, Weight = 40, Name = "Academic" },
                    new License { Code = 136, Weight = 50, Name = "Non-Practising" },
                    // Physical Therapy
                    new License { Code = 137, Weight = 10, Name = "Full" },
                    new License { Code = 138, Weight = 20, Name = "Student" },
                    new License { Code = 139, Weight = 30, Name = "Temporary" },
                    // Psychology
                    new License { Code = 140, Weight = 10, Name = "Registered Psychologist" },
                    new License { Code = 141, Weight = 20, Name = "Associate Psychologist (corrections)" },
                    new License { Code = 142, Weight = 30, Name = "School Psychologist" },
                    new License { Code = 143, Weight = 40, Name = "Psychology Assistant" },
                    new License { Code = 144, Weight = 50, Name = "Temporary (supervised)" },
                    new License { Code = 145, Weight = 60, Name = "Temporary (visitor)" },
                    new License { Code = 146, Weight = 70, Name = "Temporary (emergency)" },
                    new License { Code = 147, Weight = 80, Name = "Non-Practising" },
                    // Audiology
                    new License { Code = 148, Weight = 10, Name = "Full" },
                    new License { Code = 149, Weight = 20, Name = "Conditional" },
                    new License { Code = 150, Weight = 30, Name = "Temporary" },
                    new License { Code = 151, Weight = 40, Name = "Non-Practising" },
                    // Hearing Instrument Dispensing
                    new License { Code = 152, Weight = 10, Name = "Full" },
                    new License { Code = 153, Weight = 20, Name = "Conditional" },
                    new License { Code = 154, Weight = 30, Name = "Temporary" },
                    new License { Code = 155, Weight = 40, Name = "Non-Practising" },
                    // Speech-Language Pathology
                    new License { Code = 156, Weight = 10, Name = "Full" },
                    new License { Code = 157, Weight = 20, Name = "Conditional" },
                    new License { Code = 158, Weight = 30, Name = "Temporary" },
                    new License { Code = 159, Weight = 40, Name = "Non-Practising" },

                    // New License class for College of Complementary Health Professionals of British Columbia
                    // Chiropractic
                    new License { Code = 160, Weight = 10, Name = "Full" },
                    new License { Code = 161, Weight = 10, Name = "Student" },
                    new License { Code = 162, Weight = 10, Name = "Non-Practising" },
                    new License { Code = 163, Weight = 10, Name = "Temporary" },
                    // Massage Therapy
                    new License { Code = 164, Weight = 10, Name = "Practising" },
                    new License { Code = 165, Weight = 10, Name = "Non-Practising" },
                    // Naturopathic Medicine
                    new License { Code = 166, Weight = 10, Name = "Full" },
                    new License { Code = 167, Weight = 10, Name = "Temporary" },
                    new License { Code = 168, Weight = 10, Name = "Student" },
                    new License { Code = 169, Weight = 10, Name = "Non-Practising" },
                    // Traditional Chinese Medicine and Acupuncture
                    new License { Code = 170, Weight = 10, Name = "Full" },
                    new License { Code = 171, Weight = 10, Name = "Limited" },
                    new License { Code = 172, Weight = 10, Name = "Temporary" },
                    new License { Code = 173, Weight = 10, Name = "Student" },
                    new License { Code = 174, Weight = 10, Name = "Non-Practising" },


                    // All other colleges are assigned the "Not Displayed" Type
                    new License { Code = 64, Weight = 1, Name = "Not Displayed" },
                };
            }
        }
    }
}
