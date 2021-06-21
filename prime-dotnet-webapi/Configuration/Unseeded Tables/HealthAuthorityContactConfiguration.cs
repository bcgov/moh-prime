using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models.HealthAuthorities;

namespace Prime.Configuration
{
    public class HealthAuthorityContactConfiguration : IEntityTypeConfiguration<HealthAuthorityContact>
    {
        public void Configure(EntityTypeBuilder<HealthAuthorityContact> builder)
        {
            builder
                .HasDiscriminator();
        }
    }
}
