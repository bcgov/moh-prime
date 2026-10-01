using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DelegateDecompiler;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("StudentTypeLookup")]
    public class StudentType : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int Weight { get; set; }
    }
}
