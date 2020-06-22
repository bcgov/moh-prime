using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SignedAgreement")]
    public class SignedAgreement : BaseDocumentUpload
    {
        public int OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

    }
}
