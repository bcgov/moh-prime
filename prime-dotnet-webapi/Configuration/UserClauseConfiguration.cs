using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : IEntityTypeConfiguration<UserClause>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;
        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<UserClause> builder)
        {
            builder.HasData(
                new UserClause { Id = 1, Clause = "MOA user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EffectiveDate = DateTime.Now, EnrolleeClassification = PrimeConstants.PRIME_MOA, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new UserClause { Id = 2, Clause = "RU user clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.", EffectiveDate = DateTime.Now, EnrolleeClassification = PrimeConstants.PRIME_RU, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
        }
    }
}
