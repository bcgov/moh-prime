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
        /// <summary>
        /// Copies all values from an Enrollee to a new EnrolleeUpdateModel
        /// </summary>
        /// <param name="enrollee"></param>
        public static EnrolleeUpdateModel CopyToUpdateModel(this Enrollee enrollee)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                // Ignore read-only fields such as CurrentStatus
                ContractResolver = new OnlyWritablePropertiesResolver()
            };

            var serialized = JsonConvert.SerializeObject(enrollee, settings);
            EnrolleeUpdateModel profile = JsonConvert.DeserializeObject<EnrolleeUpdateModel>(serialized);

            // Finalize remaing fields
            profile.VerifiedAddress = enrollee.VerifiedAddress;
            profile.PhysicalAddress = enrollee.PhysicalAddress;
            profile.MailingAddress = enrollee.MailingAddress;
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
