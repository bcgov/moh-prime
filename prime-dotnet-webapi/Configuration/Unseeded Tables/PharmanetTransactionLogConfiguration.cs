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
        }
    }
}
