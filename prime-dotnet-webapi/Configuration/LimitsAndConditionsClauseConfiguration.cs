using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class LimitsAndConditionsClauseConfiguration : SeededTable<LimitsAndConditionsClause>
    {
        public override ICollection<LimitsAndConditionsClause> SeedData
        {
            get
            {
                return new[] {
                new LimitsAndConditionsClause { Id = 1, Clause = "Limit and condition 1 Lorem ipsum dolor sit amet consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod.", EffectiveDate = SeedConstants.SEEDING_DATE, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new LimitsAndConditionsClause { Id = 2, Clause = "Limit and condition 2 Adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod.", EffectiveDate = SeedConstants.SEEDING_DATE, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
                };
            }
        }
    }
}
