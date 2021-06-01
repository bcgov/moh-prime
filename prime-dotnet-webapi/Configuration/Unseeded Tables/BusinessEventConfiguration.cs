using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class BusinessEventConfiguration : IEntityTypeConfiguration<BusinessEvent>
    {
        public void Configure(EntityTypeBuilder<BusinessEvent> builder)
        {
            builder
                .HasOne(be => be.BusinessEventType)
                .WithMany(t => t.BusinessEvents)
                .HasForeignKey(be => be.BusinessEventTypeCode);
        }
    }
}
