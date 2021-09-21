using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("GisEnrolment")]
    public class GisEnrolment : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PartyId { get; set; }

        public Party Party { get; set; }

        public string LdapUsername { get; set; }

        public DateTimeOffset? LdapLoginSuccessDate { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }
    }
}
