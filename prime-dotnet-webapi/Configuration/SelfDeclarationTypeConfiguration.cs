using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class SelfDeclarationTypeConfiguration : SeededTable<SelfDeclarationType>
    {
        public override IEnumerable<SelfDeclarationType> SeedData
        {
            get
            {
                return new[] {
                    new SelfDeclarationType { Code = 1, Name = "Has Conviction", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationType { Code = 2, Name = "Has Registration Suspended", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationType { Code = 3, Name = "Has Disciplinary Action", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationType { Code = 4, Name = "Has PharmaNet Suspended", CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}
