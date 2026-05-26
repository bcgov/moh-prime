using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Flurl;

namespace Prime.Models
{
    [Table("AccessTokenRemoteAccessSite")]
    public sealed class AccessTokenRemoteAccessSite : BaseAuditable
    {

        [Key]
        public int Id { get; set; }

        public Guid EnrolmentCertificateAccessTokenId { get; set; }

        public int SiteId { get; set; }

        public Site RemoteAccessSite { get; set; }
    }
}
