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
                    new License { Code = 1,  Weight = 1,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full - Family"                                       },
                    new License { Code = 2,  Weight = 2,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full - Specialty"                                    },
                    new License { Code = 5,  Weight = 3,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Provisional - Family"                                },
                    new License { Code = 6,  Weight = 4,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Provisional - Specialty"                             },
                    new License { Code = 9,  Weight = 5,  Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Practice Setting"                      },
                    new License { Code = 8,  Weight = 6,  Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Practice Limitations"                  },
                    new License { Code = 10, Weight = 7,  Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Conditional - Disciplined"                           },
                    new License { Code = 22, Weight = 8,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Surgical Assistant"                                  },
                    new License { Code = 16, Weight = 9,  Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Clinical Observership"                               },
                    new License { Code = 7,  Weight = 10, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Academic"                                            },
                    new License { Code = 4,  Weight = 11, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Osteopathic"                                         },
                    new License { Code = 3,  Weight = 12, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Special"                                             },
                    new License { Code = 17, Weight = 13, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Visitor"                                             },
                    new License { Code = 12, Weight = 14, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Resident"                 },
                    new License { Code = 13, Weight = 15, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Resident Elective"        },
                    new License { Code = 14, Weight = 16, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Fellow"                   },
                    new License { Code = 15, Weight = 17, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Educational - Postgraduate Trainee"                  },
                    new License { Code = 11, Weight = 18, Prefix = "91", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Medical Student"                       },
                    new License { Code = 23, Weight = 19, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Administrative"                                      },
                    new License { Code = 20, Weight = 20, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Retired - Life "                                     },
                    new License { Code = 24, Weight = 21, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Assessment"                                          },
                    new License { Code = 18, Weight = 22, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Emergency - Family"                                  },
                    new License { Code = 19, Weight = 23, Prefix = "91", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Emergency - Specialty"                               },
                    new License { Code = 21, Weight = 24, Prefix = "91", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = false, Name = "Temporarily Inactive"                                },
                    new License { Code = 59, Weight = 25, Prefix = "93", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Podiatric Surgeon"                                   },
                    new License { Code = 65, Weight = 26, Prefix = "93", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Podiatric Surgeon Student (Elective)"  },
                    new License { Code = 66, Weight = 27, Prefix = "93", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Educational - Podiatric Surgeon Resident (Elective)" },
                    new License { Code = 67, Weight = 28, Prefix = "93", Manual = true,  Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  Name = "Conditional - Podiatric Surgeon Disciplined"         },

                    // Pharmacy
                    new License { Code = 25, Weight = 1,  Prefix = "P1", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Full Pharmacist"                    },
                    new License { Code = 26, Weight = 2,  Prefix = "P1", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Limited Pharmacist"                 },
                    new License { Code = 28, Weight = 3,  Prefix = "P1", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Student Pharmacist"                 },
                    new License { Code = 27, Weight = 4,  Prefix = "P1", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  Name = "Temporary Pharmacist"               },
                    new License { Code = 30, Weight = 5,  Prefix = "P1", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = false, Name = "Non-Practicing Pharmacist"          },
                    new License { Code = 29, Weight = 6,  Prefix = "T9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Pharmacy Technician"                },
                    new License { Code = 31, Weight = 7,  Prefix = "T9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Non-Practicing Pharmacy Technician" },
                    new License { Code = 68, Weight = 8,  Prefix = "T9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  Name = "Temporary Pharmacy Technician"      },

                    // Nursing
                    new License { Code = 47, Weight = 1,  Prefix = "96", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Mandatory, Name = "Practicing Nurse Practitioner"                          },
                    new License { Code = 48, Weight = 2,  Prefix = "96", Manual = true,  Validate = false, NamedInImReg = true,  LicensedToProvideCare = true,                                                 Name = "Provisional Nurse Practitioner"                         },
                    new License { Code = 51, Weight = 4,  Prefix = "96", Manual = false, Validate = true,  NamedInImReg = true,  LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Mandatory, Name = "Temporary Nurse Practitioner (Emergency)"               },
                    new License { Code = 49, Weight = 5,  Prefix = "96", Manual = true,  Validate = true,  NamedInImReg = true,  LicensedToProvideCare = false,                                                Name = "Non-Practicing Nurse Practitioner"                      },
                    new License { Code = 32, Weight = 6,  Prefix = "R9", Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Optional,  Name = "Practicing Registered Nurse"                            },
                    new License { Code = 33, Weight = 7,  Prefix = "R9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Provisional Registered Nurse"                           },
                    new License { Code = 39, Weight = 9,  Prefix = "R9", Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Optional,  Name = "Temporary Registered Nurse (Emergency)"                 },
                    new License { Code = 34, Weight = 10, Prefix = "R9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false,                                                Name = "Non-Practicing Registered Nurse"                        },
                    new License { Code = 40, Weight = 11, Prefix = "R9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Employed Student Nurse"                                 },
                    new License { Code = 35, Weight = 12, Prefix = "R9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Optional,  Name = "Practicing Licensed Graduate Nurse"                     },
                    new License { Code = 36, Weight = 13, Prefix = "R9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Provisional Licensed Graduate Nurse"                    },
                    new License { Code = 37, Weight = 14, Prefix = "R9", Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = false,                                                Name = "Non-Practicing Licensed Graduate Nurse"                 },
                    new License { Code = 41, Weight = 15, Prefix = "Y9", Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Optional,  Name = "Practicing Registered Psychiatric Nurse"                },
                    new License { Code = 42, Weight = 16, Prefix = "Y9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Provisional Registered Psychiatric Nurse"               },
                    new License { Code = 45, Weight = 18, Prefix = "Y9", Manual = false, Validate = true,  NamedInImReg = false, LicensedToProvideCare = true,  PrescriberIdType = PrescriberIdType.Optional,  Name = "Temporary Registered Psychiatric Nurse (Emergency)"     },
                    new License { Code = 43, Weight = 19, Prefix = "Y9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false,                                                Name = "Non-Practicing Registered Psychiatric Nurse"            },
                    new License { Code = 46, Weight = 20, Prefix = "Y9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Employed Student Psychiatric Nurse"                     },
                    new License { Code = 52, Weight = 21, Prefix = "L9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Practicing Licensed Practical Nurse"                    },
                    new License { Code = 53, Weight = 22, Prefix = "L9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Provisional Licensed Practical Nurse"                   },
                    new License { Code = 55, Weight = 23, Prefix = "L9", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Temporary Licensed Practical Nurse (Emergency)"         },
                    new License { Code = 54, Weight = 25, Prefix = "L9", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false,                                                Name = "Non-Practicing Licensed Practical Nurse"                },
                    new License { Code = 60, Weight = 28, Prefix = "98", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Practising Midwife"                                     },
                    new License { Code = 61, Weight = 29, Prefix = "98", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Provisional Midwife"                                    },
                    new License { Code = 62, Weight = 30, Prefix = "98", Manual = false, Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Temporary Midwife (Emergency)"                          },
                    new License { Code = 63, Weight = 31, Prefix = "98", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Non-Practising Midwife"                                 },
                    new License { Code = 69, Weight = 32, Prefix = "98", Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = true,                                                 Name = "Student Midwife"                                        },

                    // All other colleges are assigned the "Not Displayed"Type
                    new License { Code = 64, Weight = 1,  Prefix = "",   Manual = true,  Validate = false, NamedInImReg = false, LicensedToProvideCare = false, Name = "Not Displayed" },
                };
            }
        }
    }
}
