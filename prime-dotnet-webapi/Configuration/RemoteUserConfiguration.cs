using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class RemoteUserConfiguration : IEntityTypeConfiguration<RemoteUser>
    {
        public void Configure(EntityTypeBuilder<RemoteUser> builder)
        {
            builder
                .HasOne(ru => ru.Site)
                .WithMany(s => s.RemoteUsers)
                .HasForeignKey(ru => ru.SiteId);
        }
    }
}
