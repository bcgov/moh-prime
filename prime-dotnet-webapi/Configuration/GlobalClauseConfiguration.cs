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
                    new GlobalClause { Id = 1, Clause = "Global clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
