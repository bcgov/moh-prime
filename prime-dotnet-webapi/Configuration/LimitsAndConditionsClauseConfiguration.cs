using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

public class LimitsAndConditionsClauseConfiguration : IEntityTypeConfiguration<LimitsAndConditionsClause>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<LimitsAndConditionsClause> builder)
    {
        #region GlobalClauseSeed
        builder.HasData(
            new LimitsAndConditionsClause { Id = 1, Clause = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod.", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new LimitsAndConditionsClause { Id = 2, Clause = "Adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod.", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
        );
        #endregion
    }
}