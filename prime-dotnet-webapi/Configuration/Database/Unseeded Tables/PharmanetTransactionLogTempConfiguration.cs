using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PharmanetTransactionLogTempConfiguration : IEntityTypeConfiguration<PharmanetTransactionLogTemp>
    {
        public void Configure(EntityTypeBuilder<PharmanetTransactionLogTemp> builder)
        {
            // Add index to the frequently search fields
            builder
                .HasIndex(e => e.TransactionId);
            builder
                .HasIndex(e => e.UserId);
            builder
                .HasIndex(e => e.PharmacyId);
            builder
                .HasIndex(e => e.TxDateTime);
        }
    }
}
