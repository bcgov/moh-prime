using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime;
using Prime.Models;

public class UserClauseConfiguration : IEntityTypeConfiguration<UserClause>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<UserClause> builder)
    {
        builder.HasData(
            new UserClause { Id = 1, Clause = "MOA clause", EffectiveDate = DateTime.Now, EnrolleeClassification = PrimeConstants.PRIME_MOA, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE },
            new UserClause { Id = 2, Clause = "RU clause", EffectiveDate = DateTime.Now, EnrolleeClassification = PrimeConstants.PRIME_RU, CreatedUserId = SYSTEM_USER, CreatedTimeStamp = SEEDING_DATE, UpdatedUserId = SYSTEM_USER, UpdatedTimeStamp = SEEDING_DATE }
        );
    }
}
