using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    /// <summary>
    ///
    /// </summary>
    [Table("EnrolleeTransactionLog")]
    public class EnrolleeTransactionLog
    {
        [Key]
        public long Id { get; set; }
        public int EnrolleeId { get; set; }
        public Enrollee Enrollee { get; set; }
        public long PharmanetTransactionLogId { get; set; }
        public PharmanetTransactionLog PharmanetTransactionLog { get; set; }
    }
}
