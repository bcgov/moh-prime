using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("EnrolmentCertificateNote")]
    public class EnrolmentCertificateNote : IDefinable, IEnrolleeNote
    {
        [Key]
        public int? Id ,

        [JsonIgnore]
        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public string Note ,

        public DateTime NoteDate ,
    }
}
