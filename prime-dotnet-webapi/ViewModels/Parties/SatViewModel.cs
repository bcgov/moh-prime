using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class SatViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string HPDID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GivenNames { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public VerifiedAddress VerifiedAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<PartyCertification> PartyCertifications { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }
    }
}
