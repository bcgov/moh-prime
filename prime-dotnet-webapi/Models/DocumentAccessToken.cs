using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("DocumentAccessToken")]
    public sealed class DocumentAccessToken : BaseAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid DocumentGuid { get; set; }

        [NotMapped]
        public string FrontendUrl
        {
            get => $"{PrimeConstants.FRONTEND_URL}/document-access/file-download/{Id}";
        }
    }
}
