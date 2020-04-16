using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string DoingBusinessAs { get; set; }

        public DateTimeOffset? AcceptedAgreementDate { get; set; }

        public Party SigningAuthority { get; set; }

        public int SigningAuthorityId { get; set; }

        [JsonIgnore]
        public IEnumerable<Location> Locations { get; set; }

    }
}
