using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder
                .HasOne(l => l.Organization)
                .WithMany(o => o.Sites)
                .HasForeignKey(l => l.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
