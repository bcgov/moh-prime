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
        public string Log { get; set; }

        [Required]
        public LogType LogType { get; set; }
    }
}
