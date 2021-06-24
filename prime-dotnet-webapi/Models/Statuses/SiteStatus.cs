using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteStatus")]
    public class SiteStatus : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public SiteStatusType StatusType { get; set; }

        public DateTimeOffset StatusDate { get; set; }


        // TODO: Why not constructor?
        public static SiteStatus FromType(SiteStatusType siteStatusType, int siteId)
        {
            return new SiteStatus
            {
                SiteId = siteId,
                StatusType = siteStatusType,
                StatusDate = DateTimeOffset.Now
            };
        }
    }
}
