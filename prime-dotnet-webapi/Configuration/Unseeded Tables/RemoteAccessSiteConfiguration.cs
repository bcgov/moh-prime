using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class RemoteAccessSiteConfiguration : IEntityTypeConfiguration<RemoteAccessSite>
    {
        public void Configure(EntityTypeBuilder<RemoteAccessSite> builder)
        {
            builder
                .HasOne(ras => ras.Enrollee)
                .WithMany(e => e.RemoteAccessSites)
                .HasForeignKey(ras => ras.EnrolleeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
