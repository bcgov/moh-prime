using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public interface INote
    {
        [Key]
        int? Id { get; set; }

        [JsonIgnore]
        int EnrolleeId { get; set; }

        [JsonIgnore]
        Enrollee Enrollee { get; set; }

        [Required]
        string Note { get; set; }

        [Required]
        DateTime NoteDate { get; set; }
    }
}
