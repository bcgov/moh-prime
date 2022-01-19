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
                    new License { Code = 13, Weight = 15, Name = "Educational - Postgraduate Resident Elective"        },
                    new License { Code = 14, Weight = 16, Name = "Educational - Postgraduate Fellow"                   },
                    new License { Code = 15, Weight = 17, Name = "Educational - Postgraduate Trainee"                  },
                    new License { Code = 11, Weight = 18, Name = "Educational - Medical Student"                       },
                    new License { Code = 23, Weight = 19, Name = "Administrative"                                      },
                    new License { Code = 20, Weight = 20, Name = "Retired - Life "                                     },
                    new License { Code = 24, Weight = 21, Name = "Assessment"                                          },
                    new License { Code = 18, Weight = 22, Name = "Emergency - Family"                                  },
                    new License { Code = 19, Weight = 23, Name = "Emergency - Specialty"                               },
                    new License { Code = 21, Weight = 24, Name = "Temporarily Inactive"                                },
                    new License { Code = 59, Weight = 25, Name = "Podiatric Surgeon"                                   },
                    new License { Code = 65, Weight = 26, Name = "Educational - Podiatric Surgeon Student (Elective)"  },
                    new License { Code = 66, Weight = 27, Name = "Educational - Podiatric Surgeon Resident (Elective)" },
                    new License { Code = 67, Weight = 28, Name = "Conditional - Podiatric Surgeon Disciplined"         },

                    // Pharmacy
                    new License { Code = 25, Weight = 1, Name = "Full Pharmacist"                    },
                    new License { Code = 26, Weight = 2, Name = "Limited Pharmacist"                 },
                    new License { Code = 28, Weight = 3, Name = "Student Pharmacist"                 },
                    new License { Code = 27, Weight = 4, Name = "Temporary Pharmacist"               },
                    new License { Code = 30, Weight = 5, Name = "Non-Practicing Pharmacist"          },
                    new License { Code = 29, Weight = 6, Name = "Pharmacy Technician"                },
                    new License { Code = 31, Weight = 7, Name = "Non-Practicing Pharmacy Technician" },
                    new License { Code = 68, Weight = 8, Name = "Temporary Pharmacy Technician"      },

                    // Nursing
                    new License { Code = 47, Weight = 1,  Name = "Practicing Nurse Practitioner"                          },
                    new License { Code = 48, Weight = 2,  Name = "Provisional Nurse Practitioner"                         },
                    new License { Code = 51, Weight = 4,  Name = "Temporary Nurse Practitioner (Emergency)"               },
                    new License { Code = 49, Weight = 5,  Name = "Non-Practicing Nurse Practitioner"                      },
                    new License { Code = 32, Weight = 6,  Name = "Practicing Registered Nurse"                            },
                    new License { Code = 33, Weight = 7,  Name = "Provisional Registered Nurse"                           },
                    new License { Code = 39, Weight = 9,  Name = "Temporary Registered Nurse (Emergency)"                 },
                    new License { Code = 34, Weight = 10, Name = "Non-Practicing Registered Nurse"                        },
                    new License { Code = 40, Weight = 11, Name = "Employed Student Nurse"                                 },
                    new License { Code = 35, Weight = 12, Name = "Practicing Licensed Graduate Nurse"                     },
                    new License { Code = 36, Weight = 13, Name = "Provisional Licensed Graduate Nurse"                    },
                    new License { Code = 37, Weight = 14, Name = "Non-Practicing Licensed Graduate Nurse"                 },
                    new License { Code = 41, Weight = 15, Name = "Practicing Registered Psychiatric Nurse"                },
                    new License { Code = 42, Weight = 16, Name = "Provisional Registered Psychiatric Nurse"               },
                    new License { Code = 45, Weight = 18, Name = "Temporary Registered Psychiatric Nurse (Emergency)"     },
                    new License { Code = 43, Weight = 19, Name = "Non-Practicing Registered Psychiatric Nurse"            },
                    new License { Code = 46, Weight = 20, Name = "Employed Student Psychiatric Nurse"                     },
                    new License { Code = 52, Weight = 21, Name = "Practicing Licensed Practical Nurse"                    },
                    new License { Code = 53, Weight = 22, Name = "Provisional Licensed Practical Nurse"                   },
                    new License { Code = 55, Weight = 23, Name = "Temporary Licensed Practical Nurse (Emergency)"         },
                    new License { Code = 54, Weight = 25, Name = "Non-Practicing Licensed Practical Nurse"                },
                    new License { Code = 60, Weight = 28, Name = "Practising Midwife"                                     },
                    new License { Code = 61, Weight = 29, Name = "Provisional Midwife"                                    },
                    new License { Code = 62, Weight = 30, Name = "Temporary Midwife (Emergency)"                          },
                    new License { Code = 63, Weight = 31, Name = "Non-Practising Midwife"                                 },
                    new License { Code = 69, Weight = 32, Name = "Student Midwife"                                        },

                    // All other colleges are assigned the "Not Displayed" Type
                    new License { Code = 64, Weight = 1, Name = "Not Displayed" },
                };
            }
        }
    }
}
