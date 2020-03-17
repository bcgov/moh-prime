using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Feedback")]
    public class Feedback : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EnrolleeId { get; set; }

        [Required]
        public Boolean Satisfied { get; set; }

        public string Comment { get; set; }

        public Feedback(int enrolleeId, Boolean satisfied, string comment)
        {
            this.EnrolleeId = enrolleeId;
            this.Satisfied = satisfied;
            this.Comment = comment;
        }
    }
}
