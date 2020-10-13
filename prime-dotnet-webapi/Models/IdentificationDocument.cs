using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("IdentificationDocument")]
    public class IdentificationDocument : BaseDocumentUpload, IEnrolleeNavigationProperty
    {
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

    }
}
