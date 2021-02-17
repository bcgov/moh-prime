using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PartyAddressConfiguration : IEntityTypeConfiguration<PartyAddress>
    {
        public void Configure(EntityTypeBuilder<PartyAddress> builder)
        {
            builder
                .HasIndex(pa => new { pa.PartyId, pa.AddressId })
                .IsUnique();
        }
    }
}
