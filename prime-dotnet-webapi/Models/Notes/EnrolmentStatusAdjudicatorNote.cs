using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatusAdjudicatorNote")]
    public class EnrolmentStatusAdjudicatorNote : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolmentStatusId { get; set; }

        [JsonIgnore]
        public EnrolmentStatus EnrolmentStatus { get; set; }

        public int AdjudicatorNoteId { get; set; }


        public AdjudicatorNote AdjudicatorNote { get; set; }
    }
}
