using Newtonsoft.Json;

namespace Prime.Models
{
    public interface IEnrolleeNavigationProperty
    {
        [JsonIgnore]
        int EnrolleeId { get; set; }

        [JsonIgnore]
        Enrollee Enrollee { get; set; }
    }
}
