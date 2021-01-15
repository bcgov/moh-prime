using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeNote")]
    public class EnrolleeNote : BaseAdjudicatorNote, IBaseEnrolleeNote
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [JsonIgnore]
        public EnrolmentStatusReference EnrolmentStatusReference { get; set; }

        [JsonIgnore]
        public EnrolmentEscalation EnrolmentEscalation { get; set; }
    }
}
