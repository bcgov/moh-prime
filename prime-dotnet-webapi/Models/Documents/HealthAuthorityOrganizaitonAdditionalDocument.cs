using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("HealthAuthorityOrganizationAdditionalDocument")]
    public class HealthAuthorityOrganizationAdditionalDocument : BaseDocumentUpload
    {
        public string Note { get; set; }
    }
}
