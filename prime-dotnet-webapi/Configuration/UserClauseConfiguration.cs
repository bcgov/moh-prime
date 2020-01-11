using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : IEntityTypeConfiguration<UserClause>
    {
        public void Configure(EntityTypeBuilder<UserClause> builder)
        {
            builder.HasData(
                new UserClause { Id = 1, Clause = "MOA user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EnrolleeClassification = PrimeConstants.PRIME_MOA, EffectiveDate = SeedConstants.SEEDING_DATE, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE },
                new UserClause { Id = 2, Clause = "RU user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SeedConstants.SEEDING_DATE, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}
