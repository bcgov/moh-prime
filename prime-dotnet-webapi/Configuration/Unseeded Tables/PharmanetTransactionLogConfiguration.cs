using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PharmanetTransactionLogConfiguration : IEntityTypeConfiguration<PharmanetTransactionLog>
    {
        public void Configure(EntityTypeBuilder<PharmanetTransactionLog> builder)
        {
            // Will frequently search by these fields
            builder
                .HasIndex(e => e.UserId);
            builder
                .HasIndex(e => e.PharmacyId);
            builder
                .HasIndex(e => e.TxDateTime);

            // Defaults for audit columns
            builder.Property(e => e.CreatedUserId)
                .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));
            builder.Property(e => e.UpdatedUserId)
                .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));
            builder.Property(e => e.CreatedTimeStamp)
                .HasDefaultValueSql("current_timestamp");
            builder.Property(e => e.UpdatedTimeStamp)
                .HasDefaultValueSql("current_timestamp");
        }
    }
}
