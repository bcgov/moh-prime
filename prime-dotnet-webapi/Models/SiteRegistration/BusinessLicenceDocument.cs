using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("BusinessLicenceDocument")]
    public class BusinessLicenceDocument : BaseDocumentUpload
    {
        public int BusinessLicenceId { get; set; }

        [JsonIgnore]
        public BusinessLicence BusinessLicence { get; set; }
    }
}
