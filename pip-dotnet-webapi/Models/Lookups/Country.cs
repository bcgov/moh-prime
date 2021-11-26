using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pidp.Models.Lookups
{
    public enum CountryCode
    {
        Canada = 1,
        UnitedStates
    }

    [Table("CountryLookup")]
    public class Country
    {
        [Key]
        public CountryCode Code { get; set; }

        public string Name { get; set; } = "";
    }

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public virtual void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(new[]
            {
                new Country { Code = CountryCode.Canada,       Name = "Canada"        },
                new Country { Code = CountryCode.UnitedStates, Name = "United States" }
            });
        }
    }
}
