using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class PracticeConfiguration : SeededTable<Practice>
    {
        public override ICollection<Practice> SeedData
        {
            get
            {
                return new[] {
                new Practice { Code = 1, Name = "Remote Practice", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Practice { Code = 2, Name = "Reproductive Health - STI", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Practice { Code = 3, Name = "Reproductive Health - Contraceptive Management", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new Practice { Code = 4, Name = "First Call", CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}
