using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolleeProfileVersionConfiguration : IEntityTypeConfiguration<EnrolleeProfileVersion>
    {
        // private DefaultContractResolver _contractResolver = new DefaultContractResolver
        // {
        //     NamingStrategy = new CamelCaseNamingStrategy()
        // };

        public void Configure(EntityTypeBuilder<EnrolleeProfileVersion> builder)
        {
            builder
                .HasOne(epf => epf.Enrollee)
                .WithMany(e => e.EnrolleeProfileVersions)
                .HasForeignKey(epf => epf.EnrolleeId);

            builder
                .Property(epf => epf.ProfileSnapshot)
                .HasColumnType("json")
                .HasConversion(
                    ps => JsonConvert.SerializeObject(ps, new JsonSerializerSettings
                    {
                        // ContractResolver = _contractResolver,
                        // ContractResolver = new CamelCasePropertyNamesContractResolver(),
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
