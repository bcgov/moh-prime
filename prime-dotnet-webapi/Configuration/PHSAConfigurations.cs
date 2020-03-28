using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Prime.Models;

namespace Prime.Configuration
{
    public class PHSAConfiguration : IEntityTypeConfiguration<PHSA>
    {
        public void Configure(EntityTypeBuilder<PHSA> builder)
        {
            builder
                .Property(phsa => phsa.JsonBody)
                .HasColumnType("json")
                .HasConversion(
                    ps => JsonConvert.SerializeObject(ps, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }),
                    ps => JsonConvert.DeserializeObject<JObject>(ps, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    })
                );
        }
    }
}
