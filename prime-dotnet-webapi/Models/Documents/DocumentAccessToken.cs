using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Flurl;

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
        public string DownloadUrl
        {
            get => Url.Combine(PrimeConfiguration.Current.BackendUrl, "document-access", Id.ToString());
        }
    }
}
