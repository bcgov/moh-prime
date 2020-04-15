using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public bool HoursWeekend { get; set; }

        public bool Hours24 { get; set; }

        public string HoursSpecial { get; set; }

        public Party AdministratorPharmaNet { get; set; }

        public int AdministratorPharmaNetId { get; set; }

        public Party PrivacyOfficer { get; set; }

        public int PrivacyOfficerId { get; set; }

        public Party TechnicalSupport { get; set; }

        public int TechnicalSupportId { get; set; }

        public string DoingBusinessAs { get; set; }
    }
}
