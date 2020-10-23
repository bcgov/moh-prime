using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SignedAgreementDocument")]
    public class SignedAgreementDocument : BaseDocumentUpload
    {
        public int AgreementId { get; set; }

        [JsonIgnore]
        public Agreement Agreement { get; set; }
    }
}
