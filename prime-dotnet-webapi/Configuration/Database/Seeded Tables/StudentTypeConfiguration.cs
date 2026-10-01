using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class StudentTypeConfiguration : SeededTable<StudentType>
    {
        public override IEnumerable<StudentType> SeedData
        {
            get
            {
                return [
                    new StudentType { Code = 1,  Name = "Student Nurse Practitioner", Weight = 10                            },
                    new StudentType { Code = 2,  Name = "Student Registered Nurse", Weight = 20                              },
                    new StudentType { Code = 3,  Name = "Student Registered Psychiatric Nurse", Weight = 30                  },
                    new StudentType { Code = 4,  Name = "Student Licensed Practical Nurse", Weight = 40                      },
                    new StudentType { Code = 5,  Name = "Student Midwife", Weight = 50                                       },
                ];
            }
        }
    }
}

