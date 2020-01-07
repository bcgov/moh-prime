using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

public class LicenceClassClauseConfiguration : IEntityTypeConfiguration<LicenceClassClause>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<LicenceClassClause> builder)
    {
        #region GlobalClauseSeed
        builder.HasData(
            new LicenceClassClause { Id = 1, Clause = "Consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new LicenceClassClause { Id = 2, Clause = "Rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!", EffectiveDate = DateTime.Now, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
        );
        #endregion
    }
}