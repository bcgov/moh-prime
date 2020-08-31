using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.DbViews
{
    [Table("NewestAgreements")]
    public class NewestAgreement
    {
        public int Id { get; set; }
    }
}
