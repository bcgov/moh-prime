using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("EnrolleeAdjudicationDocument")]
    public class EnrolleeAdjudicationDocument : BaseDocumentUpload, IAdjudicationDocument
    {
        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }
    }
}
