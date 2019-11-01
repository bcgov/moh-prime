using Newtonsoft.Json;

namespace Prime.Models
{
    public interface IEnrolmentNavigationProperty
    {
        [JsonIgnore]
        int EnrolmentId { get; set; }

        [JsonIgnore]
        Enrolment Enrolment { get; set; }
    }
}
