using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
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
    }
}
