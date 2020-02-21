using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("BusinessEvent")]
    public class BusinessEvent : BaseAuditable
    {
        [Key]
        public int? Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int? AdminId { get; set; }

        [JsonIgnore]
        public Admin Admin { get; set; }

        public short BusinessEventTypeCode { get; set; }

        [JsonIgnore]
        public BusinessEventType BusinessEventType { get; set; }

        public string Description { get; set; }

        public DateTime? EventDate { get; set; }
    }
}
