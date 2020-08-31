using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prime.Models;
using Prime.ViewModels;

namespace PrimeTests
{
    public static class EnrolleeExtensions
    {
        public static EnrolleeUpdateModel ToViewModel(this Enrollee enrollee)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                // Ignore read-only fields such as CurrentStatus
                ContractResolver = new OnlyWritablePropertiesResolver()
            };

            var serialized = JsonConvert.SerializeObject(enrollee, settings);
            EnrolleeUpdateModel profile = JsonConvert.DeserializeObject<EnrolleeUpdateModel>(serialized);
            profile.IdentityAssuranceLevel = enrollee.IdentityAssuranceLevel;
            profile.IdentityProvider = enrollee.IdentityProvider;
            return profile;
        }

        private class OnlyWritablePropertiesResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
                return props.Where(p => p.Writable).ToList();
            }
        }
    }
}
