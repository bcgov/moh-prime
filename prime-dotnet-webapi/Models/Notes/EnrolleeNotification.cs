using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeNotification")]
    public class EnrolleeNotification : BaseNotification
    {
        public int EnrolleeNoteId { get; set; }
        [JsonIgnore]
        public EnrolleeNote EnrolleeNote { get; set; }
    }
}
