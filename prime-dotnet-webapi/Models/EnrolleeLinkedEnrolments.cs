using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Prime.Models
{
    [Table("EnrolleeLinkedEnrolments")]
    public class EnrolleeLinkedEnrolments : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolmentId { get; set; }

        public int PaperEnrolmentId { get; set; }

        public string UserProvidedGpid { get; set; }

        public DateTime EnrolmentCreationDate { get; set; }
    }
}
