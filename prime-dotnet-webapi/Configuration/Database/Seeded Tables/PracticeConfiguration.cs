using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PracticeConfiguration : SeededTable<Practice>
    {
        public override IEnumerable<Practice> SeedData
        {
            get
            {
                return new[] {
                    new Practice { Code = 1, Name = "Remote Practice"                                       },
                    new Practice { Code = 2, Name = "Reproductive Health - Sexually Transmitted Infections" },
                    new Practice { Code = 3, Name = "Reproductive Health - Contraceptive Management"        },
                    new Practice { Code = 4, Name = "First Call"                                            }
                };
            }
        }
    }
}
