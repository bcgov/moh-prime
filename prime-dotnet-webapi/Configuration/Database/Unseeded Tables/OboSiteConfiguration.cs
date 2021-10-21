using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Database
{
    public class OboSiteConfiguration : IEntityTypeConfiguration<OboSite>
    {
        public void Configure(EntityTypeBuilder<OboSite> builder)
        {
            builder
                .HasOne(obo => obo.PhysicalAddress)
                .WithMany()
                .IsRequired();
        }
    }
}
