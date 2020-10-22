using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AccessAgreementNote")]
    public class AccessAgreementNote : BaseAdjudicatorNote, IBaseEnrolleeNote
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }
    }
}
