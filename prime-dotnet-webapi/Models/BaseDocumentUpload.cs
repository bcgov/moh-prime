using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class BaseDocumentUpload : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public Guid DocumentGuid { get; set; }

        public string Filename { get; set; }

        public DateTimeOffset UploadedDate { get; set; }
    }
}
