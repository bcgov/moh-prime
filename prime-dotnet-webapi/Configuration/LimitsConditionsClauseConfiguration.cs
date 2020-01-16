using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

public class LimitsConditionsClauseConfiguration : IEntityTypeConfiguration<LimitsConditionsClause>
{
    private readonly Guid SYSTEM_USER = Guid.Empty;

    private readonly DateTime SEEDING_DATE = DateTime.Now;

    public void Configure(EntityTypeBuilder<LimitsConditionsClause> builder)
    {

    }
}
