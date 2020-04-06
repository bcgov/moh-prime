using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class GlobalClauseConfiguration : SeededTable<GlobalClause>
    {
        public override ICollection<GlobalClause> SeedData
        {
            get
            {
                return new[] {
                    new GlobalClause { Id = 1, Clause = "", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
