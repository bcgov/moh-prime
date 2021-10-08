using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder
                .Property(epf => epf.ProfileSnapshot)
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
