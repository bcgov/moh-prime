using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class RemoteAccessLocationConfiguration : IEntityTypeConfiguration<RemoteAccessLocation>
    {
        public void Configure(EntityTypeBuilder<RemoteAccessLocation> builder)
        {
            builder
                .HasOne(ral => ral.Enrollee)
                .WithMany(e => e.RemoteAccessLocations)
                .HasForeignKey(ral => ral.EnrolleeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
