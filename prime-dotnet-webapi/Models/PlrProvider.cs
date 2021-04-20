using System;
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

        public string[] Expertise { get; set; }

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

        public string[] Credentials { get; set; }

        public string TelephoneAreaCode { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxAreaCode { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string ConditionCode { get; set; }


        /// <summary>
        /// All properties should be update-able except for the keys:  Id and Ipc
        /// </summary>
        /// <param name="newer"></param>
        public void Update(PlrProvider newer)
        {
            this.Address1Line1 = newer.Address1Line1;
            this.Address1Line2 = newer.Address1Line2;
            this.Address1Line3 = newer.Address1Line3;
            this.Address1StartDate = newer.Address1StartDate;
            this.City1 = newer.City1;
            this.Province1 = newer.Province1;
            this.Country1 = newer.Country1;
            this.PostalCode1 = newer.PostalCode1;
            this.Address1StartDate = newer.Address1StartDate;

            this.Address2Line1 = newer.Address2Line1;
            this.Address2Line2 = newer.Address2Line2;
            this.Address2Line3 = newer.Address2Line3;
            this.Address2StartDate = newer.Address2StartDate;
            this.City2 = newer.City2;
            this.Province2 = newer.Province2;
            this.Country2 = newer.Country2;
            this.PostalCode2 = newer.PostalCode2;
            this.Address2StartDate = newer.Address2StartDate;

            this.CollegeId = newer.CollegeId;
            this.ConditionCode = newer.ConditionCode;
            this.Credentials = newer.Credentials;
            this.DateOfBirth = newer.DateOfBirth;
            this.Email = newer.Email;
            this.Expertise = newer.Expertise;
            this.FaxAreaCode = newer.FaxAreaCode;
            this.FirstName = newer.FirstName;
            this.Gender = newer.Gender;
            this.IdentifierType = newer.IdentifierType;
            this.Languages = newer.Languages;
            this.LastName = newer.LastName;
            this.MspId = newer.MspId;
            this.NamePrefix = newer.NamePrefix;
            this.ProviderRoleType = newer.ProviderRoleType;
            this.SecondName = newer.SecondName;
            this.StatusCode = newer.StatusCode;
            this.StatusExpiryDate = newer.StatusExpiryDate;
            this.StatusReasonCode = newer.StatusReasonCode;
            this.StatusStartDate = newer.StatusStartDate;
            this.Suffix = newer.Suffix;
            this.TelephoneAreaCode = newer.TelephoneAreaCode;
            this.TelephoneNumber = newer.TelephoneNumber;
            this.ThirdName = newer.ThirdName;
        }

    }
}
