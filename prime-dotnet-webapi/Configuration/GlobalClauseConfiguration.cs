using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class GlobalClauseConfiguration : IEntityTypeConfiguration<GlobalClause>
    {
        private readonly Guid SYSTEM_USER = Guid.Empty;

        private readonly DateTime SEEDING_DATE = DateTime.Now;

        public void Configure(EntityTypeBuilder<GlobalClause> builder)
        {
            #region GlobalClauseSeed
            builder.HasData(
                new GlobalClause { Clause = "Global clause", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
            );
            #endregion
        }
    }
}