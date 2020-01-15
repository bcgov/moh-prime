using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("EnrolmentCertificateAccessToken")]
    public sealed class EnrolmentCertificateAccessToken : IDefinable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id ,

        [Required]
        public int? EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        public DateTime Expires ,

        public int ViewCount ,

        public Boolean Active ,
    }
}
