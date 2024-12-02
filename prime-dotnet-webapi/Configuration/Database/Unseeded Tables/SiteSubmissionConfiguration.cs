using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class SiteSubmissionConfiguration : IEntityTypeConfiguration<SiteSubmission>
    {
        public void Configure(EntityTypeBuilder<SiteSubmission> builder)
        {
            builder
                .Property(ss => ss.ProfileSnapshot)
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
