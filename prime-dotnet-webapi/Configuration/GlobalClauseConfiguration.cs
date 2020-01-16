using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class GlobalClauseConfiguration : IEntityTypeConfiguration<GlobalClause>
    {
        public void Configure(EntityTypeBuilder<GlobalClause> builder)
        {
            builder.HasData(
                new GlobalClause { Id = 1, Clause = "Global clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EffectiveDate = SeedConstants.SEEDING_DATE, CreatedTimeStamp = SeedConstants.SEEDING_DATE, UpdatedTimeStamp = SeedConstants.SEEDING_DATE }
            );
        }
    }
}
