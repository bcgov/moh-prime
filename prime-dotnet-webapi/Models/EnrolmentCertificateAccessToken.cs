using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentCertificateAccessToken")]
    public sealed class EnrolmentCertificateAccessToken : BaseAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        [Required]
        public int? EnrolleeId {get; set;}

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int ViewCount { get; set; }

        public Boolean Active { get; set; }
    }
}
