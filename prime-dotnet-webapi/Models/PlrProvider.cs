using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.Plr;

namespace Prime.Models
{
    /// <summary>We rely on PLR's database integrity checks rather than enforcing any in our system.</summary>
    [Table("PlrProvider")]
    public class PlrProvider : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        /// <summary>PLR's internal identifier, Internal Party Code.</summary>
        public string Ipc { get; set; }

        /// <summary>A physical person in multiple roles will have a common CPN (PLR's Common Party Number) for each PLR record representing that role, e.g. a person who is both a MD and Pharmacist</summary>
        public string Cpn { get; set; }

        /// <summary>The type of identifier that <c>CollegeId</c> represents.</summary>
        public string IdentifierType { get; set; }

        public string CollegeId { get; set; }

        public string ProviderRoleType { get; set; }

        [JsonIgnore]
        public PlrRoleType ProviderRole { get; set; }

        /// <summary>HIBC's Ministry Practitioner ID.</summary>
        public string MspId { get; set; }

        public string NamePrefix { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string StatusCode { get; set; }

        public string StatusReasonCode { get; set; }

        [JsonIgnore]
        public PlrStatusReason StatusReason { get; set; }

        public DateTime? StatusStartDate { get; set; }

        public DateTime? StatusExpiryDate { get; set; }

        public ICollection<string> Expertise { get; set; }

        public string Address1Line1 { get; set; }

        public string Address1Line2 { get; set; }

        public string Address1Line3 { get; set; }

        public string City1 { get; set; }

        public string Province1 { get; set; }

        public string Country1 { get; set; }

        public string PostalCode1 { get; set; }

        public DateTime? Address1StartDate { get; set; }

        public ICollection<string> Credentials { get; set; }

        public string TelephoneAreaCode { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxAreaCode { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string ConditionCode { get; set; }

        public DateTime? ConditionStartDate { get; set; }

        public DateTime? ConditionEndDate { get; set; }
    }
}
