using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public class SantaClaus : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string WhatDoesSantaSay
        {
            get => "Ho ho ho ho!!! Merrrry Christmas!";
        }

        public string WhatDoesSantaWant
        {
            get => "Milk and Cookies!";
        }

        public string AccessClausIsProvided
        {
            get => "Chimney";
        }

        public string WhoStoleChristmas
        {
            get => "The Grinch!";
        }

        public ICollection<Enrollee> NaughtyList { get; set; }

        public ICollection<Enrollee> NiceList { get; set; }
    }
}
