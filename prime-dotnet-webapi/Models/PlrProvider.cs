using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>The type of identifier that <c>CollegeId</c> represents.</summary>
        public string IdentifierType { get; set; }

        public string CollegeId { get; set; }

        public string ProviderRoleType { get; set; }

        /// <summary>HIBC's Ministry Practitioner ID.</summary>
        public string MspId { get; set; }

        public string NamePrefix { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string StatusCode { get; set; }

        public string StatusReasonCode { get; set; }

        public DateTime StatusStartDate { get; set; }

        public DateTime StatusExpiryDate { get; set; }

        public ICollection<string> Expertise { get; set; }

        public string Languages { get; set; }

        public string Address1Line1 { get; set; }

        public string Address1Line2 { get; set; }

        public string Address1Line3 { get; set; }

        public string City1 { get; set; }

        public string Province1 { get; set; }

        public string Country1 { get; set; }

        public string PostalCode1 { get; set; }

        public DateTime Address1StartDate { get; set; }

        public string Address2Line1 { get; set; }

        public string Address2Line2 { get; set; }

        public string Address2Line3 { get; set; }

        public string City2 { get; set; }

        public string Province2 { get; set; }

        public string Country2 { get; set; }

        public string PostalCode2 { get; set; }

        public DateTime Address2StartDate { get; set; }

        public ICollection<string> Credentials { get; set; }

        public string TelephoneAreaCode { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxAreaCode { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string ConditionCode { get; set; }
    }
}
