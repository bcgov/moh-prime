using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolleeAddressConfiguration : IEntityTypeConfiguration<EnrolleeAddress>
    {
        public void Configure(EntityTypeBuilder<EnrolleeAddress> builder)
        {
            builder
                .HasIndex(ea => new { ea.EnrolleeId, ea.AddressId })
                .IsUnique();
        }
    }
}
