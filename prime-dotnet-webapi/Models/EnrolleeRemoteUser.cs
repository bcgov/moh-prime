using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeRemoteUser")]
    public class EnrolleeRemoteUser : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int RemoteUserId { get; set; }

        [JsonIgnore]
        public RemoteUser RemoteUser { get; set; }

    }
}
