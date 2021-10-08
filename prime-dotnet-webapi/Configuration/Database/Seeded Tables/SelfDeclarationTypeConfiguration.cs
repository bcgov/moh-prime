using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class SelfDeclarationTypeConfiguration : SeededTable<SelfDeclarationType>
    {
        public override IEnumerable<SelfDeclarationType> SeedData
        {
            get
            {
                return new[] {
                    new SelfDeclarationType { Code = 1, Name = "Has Conviction"             },
                    new SelfDeclarationType { Code = 2, Name = "Has Registration Suspended" },
                    new SelfDeclarationType { Code = 3, Name = "Has Disciplinary Action"    },
                    new SelfDeclarationType { Code = 4, Name = "Has PharmaNet Suspended"    },
                };
            }
        }
    }
}
