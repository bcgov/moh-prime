using System.Linq;

using Newtonsoft.Json;

using Prime.Models;
using Prime.ViewModels;

namespace PrimeTests
{
    public static class EnrolleeExtensions
    {
        public static EnrolleeProfileViewModel ToViewModel(this Enrollee enrollee)
        {
            var serialized = JsonConvert.SerializeObject(enrollee);
            EnrolleeProfileViewModel profile = JsonConvert.DeserializeObject<EnrolleeProfileViewModel>(serialized);
            profile.IdentityAssuranceLevel = enrollee.IdentityAssuranceLevel;
            return profile;
        }
    }
}
