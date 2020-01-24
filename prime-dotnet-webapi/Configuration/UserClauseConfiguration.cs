using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {
        public override ICollection<UserClause> SeedData
        {
            get
            {
                return new[] {
                    new UserClause { Id = 1, Clause = "OBO user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = "RU user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
