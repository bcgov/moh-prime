using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("Phsa")]
    public class PHSA : BaseAuditable
    {

        [Key]
        public int Id { get; set; }

        public DateTimeOffset SubmissionTime { get; set; }

        public JObject JsonBody { get; set; }

    }
}
