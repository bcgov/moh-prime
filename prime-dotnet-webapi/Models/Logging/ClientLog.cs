using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{

    [Table("ClientLog")]
    public class ClientLog : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public string Data { get; set; }

        [Required]
        public LogType LogType { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
