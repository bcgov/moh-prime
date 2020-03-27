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
            return JsonConvert.DeserializeObject<EnrolleeProfileViewModel>(serialized);
        }
    }
}
