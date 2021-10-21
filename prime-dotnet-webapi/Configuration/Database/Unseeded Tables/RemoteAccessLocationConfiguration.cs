using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Database
{
    public class RemoteAccessLocationConfiguration : IEntityTypeConfiguration<RemoteAccessLocation>
    {
        public void Configure(EntityTypeBuilder<RemoteAccessLocation> builder)
        {
            builder
                .HasOne(location => location.PhysicalAddress)
                .WithMany()
                .IsRequired();
        }
    }
}
