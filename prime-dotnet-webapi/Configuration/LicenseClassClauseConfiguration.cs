using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class LicenseClassClauseConfiguration : SeededTable<LicenseClassClause>
    {
        public override ICollection<LicenseClassClause> SeedData
        {
            get
            {
                return new[] {
                    new LicenseClassClause { Id = 1, Clause = "License class clause 1 Consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new LicenseClassClause { Id = 2, Clause = "License class clause 2 Rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
