using System;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class GisViewModel
    {
        public int Id { get; set; }
        public string LdapUsername { get; set; }
        public DateTimeOffset LdapLoginSuccessDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public Party Party { get; set; }
    }
}
