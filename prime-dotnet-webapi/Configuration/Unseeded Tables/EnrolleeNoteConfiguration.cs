using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolleeNoteConfiguration : IEntityTypeConfiguration<EnrolleeNote>
    {
        public void Configure(EntityTypeBuilder<EnrolleeNote> builder)
        {
            builder
                .HasOne(an => an.Enrollee)
                .WithMany(e => e.AdjudicatorNotes)
                .HasForeignKey(an => an.EnrolleeId);
        }
    }
}
