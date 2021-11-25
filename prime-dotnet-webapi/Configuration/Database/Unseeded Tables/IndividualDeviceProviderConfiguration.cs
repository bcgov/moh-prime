using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class IndividualDeviceProviderConfiguration : IEntityTypeConfiguration<IndividualDeviceProvider>
    {
        public void Configure(EntityTypeBuilder<IndividualDeviceProvider> builder)
        {
            builder
                .HasOne<CommunitySite>(idp => idp.CommunitySite)
                .WithMany()
                .HasForeignKey(idp => idp.CommunitySiteId);
        }
    }
}
