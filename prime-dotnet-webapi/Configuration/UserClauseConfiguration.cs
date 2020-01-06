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
            #region UserClauseSeed
            builder.HasData(
                new UserClause { Clause = "MOA", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
                new UserClause { Clause = "OBO", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
            #endregion
        }
    }
}