using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Gis")]
    public class Gis : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PartyId { get; set; }

        public Party Party { get; set; }

        public string Organization { get; set; }

        public string Role { get; set; }
    }
}
