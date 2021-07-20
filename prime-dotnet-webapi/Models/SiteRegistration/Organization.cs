using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using DelegateDecompiler;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable, IAgreeable
    {
        public Organization()
        {
            // Initialize collections to prevent null exception on computed properties
            // like `HasClaim`
            Claims = new List<OrganizationClaim>();
        }

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

        [JsonIgnore]
        public ICollection<OrganizationClaim> Claims { get; set; }

        [NotMapped]
        [Computed]
        public int DisplayId
        {
            get => Id + DISPLAY_OFFSET;
        }

        [NotMapped]
        public bool HasAcceptedAgreement
        {
            get => Agreements?.Any(a => a.AcceptedDate.HasValue) ?? false;
        }

        [NotMapped]
        public bool HasSubmittedSite
        {
            get => Sites?.Any(s => s.SubmittedDate.HasValue) ?? false;
        }

        [NotMapped]
        [Computed]
        public bool HasClaim
        {
            get => Claims.Any();
        }
    }
}
