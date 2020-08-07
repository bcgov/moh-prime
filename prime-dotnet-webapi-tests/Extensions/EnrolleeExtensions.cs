using System.Linq;

using Newtonsoft.Json;

using Prime.Models;
using Prime.ViewModels;

namespace PrimeTests
{
    public static class EnrolleeExtensions
    {
        public static EnrolleeUpdateModel ToViewModel(this Enrollee enrollee)
        {
            var serialized = JsonConvert.SerializeObject(enrollee);
            EnrolleeUpdateModel profile = JsonConvert.DeserializeObject<EnrolleeUpdateModel>(serialized);
            profile.IdentityAssuranceLevel = enrollee.IdentityAssuranceLevel;
            return profile;
        }
    }
}
