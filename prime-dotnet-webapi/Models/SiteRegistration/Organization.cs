using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable, IAgreeable
    {
        public const int DISPLAY_OFFSET = 1000;

        [Key]
        public int Id { get; set; }

        public string RegistrationId { get; set; }

        public string Name { get; set; }

        public string DoingBusinessAs { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public Party SigningAuthority { get; set; }

        public int SigningAuthorityId { get; set; }

        [JsonIgnore]
        public ICollection<Site> Sites { get; set; }

        [JsonIgnore]
        public ICollection<Agreement> Agreements { get; set; }

        [NotMapped]
        public int SiteCount
        {
            get => (this.Sites == null) ? 0 : this.Sites.Count;
        }

        [NotMapped]
        public int DisplayId
        {
            get => Id + DISPLAY_OFFSET;
        }
    }
}
