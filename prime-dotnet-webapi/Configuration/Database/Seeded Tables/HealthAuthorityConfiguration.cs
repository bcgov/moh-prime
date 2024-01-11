using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class HealthAuthorityConfiguration : SeededTable<HealthAuthority>
    {
        public override IEnumerable<HealthAuthority> SeedData
        {
            get
            {
                return new[] {
                    new HealthAuthority { Code = HealthAuthorityCode.NorthernHealth,                    Name = "Northern Health",                      Passcode = "HA@PRIME"},
                    new HealthAuthority { Code = HealthAuthorityCode.InteriorHealth,                    Name = "Interior Health",                      Passcode = "HA@PRIME"},
                    new HealthAuthority { Code = HealthAuthorityCode.VancouverCoastalHealth,            Name = "Vancouver Coastal Health",             Passcode = "HA@PRIME"},
                    new HealthAuthority { Code = HealthAuthorityCode.IslandHealth,                      Name = "Island Health",                        Passcode = "HA@PRIME"},
                    new HealthAuthority { Code = HealthAuthorityCode.FraserHealth,                      Name = "Fraser Health",                        Passcode = "HA@PRIME"},
                    new HealthAuthority { Code = HealthAuthorityCode.ProvincialHealthServicesAuthority, Name = "Provincial Health Services Authority", Passcode = "HA@PRIME"}
                };
            }
        }
    }
}
