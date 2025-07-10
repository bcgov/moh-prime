using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DelegateDecompiler;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("RemoteUser")]
    public class RemoteUser : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Whether there was an attempt to notify Remote User by email
        /// </summary>
        public bool Notified { get; set; }

        [NotMapped]
        [Computed]
        public DateTimeOffset? CreatedDate
        {
            get => this.CreatedTimeStamp;
        }

        public RemoteUserCertification RemoteUserCertification { get; set; }
    }
}
