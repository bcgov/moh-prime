using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{

    [Table("FrontEndLog")]
    public class FrontEndLog : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Msg { get; set; }

        public string Data { get; set; }

        [Required]
        public LogType LogType { get; set; }
    }
}
