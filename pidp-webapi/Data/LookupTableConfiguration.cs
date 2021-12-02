using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Pidp.Models.Lookups;

namespace Pidp.Data
{
    public abstract class LookupTableConfiguration<TEntity, TGenerator> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
        where TGenerator : ILookupDataGenerator<TEntity>, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasData(new TGenerator().Generate());
        }
    }
}
