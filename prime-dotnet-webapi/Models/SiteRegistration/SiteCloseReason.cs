using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteCloseReasonLookup")]
    public sealed class SiteCloseReason : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        public string Name { get; set; }
    }
}
