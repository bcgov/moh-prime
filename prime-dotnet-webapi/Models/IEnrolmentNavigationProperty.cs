using Newtonsoft.Json;

namespace Prime.Models
{
    public interface IEnrolmentNavigationProperty
    {
        [JsonIgnore]
        int EnrolleeId { get; set; }

        [JsonIgnore]
        Enrollee Enrollee { get; set; }
    }
}
