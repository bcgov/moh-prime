using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Flurl;
using System.Collections.Generic;

namespace Prime.Models
{
    [Table("EnrolmentCertificateAccessToken")]
    public sealed class EnrolmentCertificateAccessToken : BaseAuditable
    {
        public static int MaxViews { get => 3; }
        public static TimeSpan Lifespan { get => TimeSpan.FromDays(10); }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public DateTimeOffset Expires { get; set; }

        public int ViewCount { get; set; }

        public bool Active { get; set; }

        public int? CareSettingCode { get; set; }

        public int? HealthAuthorityCode { get; set; }

        public ICollection<AccessTokenRemoteAccessSite> RemoteAccessSites { get; set; }

        [NotMapped]
        public string FrontendUrl
        {
            get => Url.Combine(PrimeConfiguration.Current.FrontendUrl, "provisioner-access", Id.ToString());
        }
    }
}
