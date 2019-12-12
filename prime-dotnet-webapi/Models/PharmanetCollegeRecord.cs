using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public class PharmanetCollegeRecord
    {
        public string applicationUUID { get; set; }
        public string firstName { get; set; }
        public string middleInitial { get; set; }
        public string lastName { get; set; }
        public DateTime dateofBirth { get; set; }
        public string status { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
