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
                    new LicenseClassClause { Id = 1, Clause = "", Type = "Dispense", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new LicenseClassClause { Id = 2, Clause = "", Type = "Prescribe", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
