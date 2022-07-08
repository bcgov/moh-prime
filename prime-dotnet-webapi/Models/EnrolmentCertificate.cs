using System;
using System.Linq;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public sealed class EnrolmentCertificate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public string GPID { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
        public IEnumerable<CareSetting> CareSettings { get; set; }
        public AgreementGroup? Group { get; set; }

        /// <summary>
        /// Identify which college at a high-level, e.g. BC College of Nurses and Midwives (BCCNM)
        /// </summary>
        public int CollegeCode { get; set; }
        /// <summary>
        /// Also known as College Prefix
        /// </summary>
        public string CollegeId { get; set; }
        public int LicenseCode { get; set; }
        public string CollegeLicenseNumber { get; set; }
        public string PharmaNetId { get; set; }




        public static EnrolmentCertificate Create(Enrollee enrollee)
        {
            return new EnrolmentCertificate
            {
                FirstName = enrollee.FirstName,
                LastName = enrollee.LastName,
                PreferredFirstName = enrollee.PreferredFirstName,
                PreferredMiddleName = enrollee.PreferredMiddleName,
                PreferredLastName = enrollee.PreferredLastName,
                GPID = enrollee.GPID,
                ExpiryDate = enrollee.ExpiryDate,
                CareSettings = enrollee.EnrolleeCareSettings.Select(org => org.CareSetting),
                Group = enrollee.Agreements.OrderByDescending(a => a.CreatedDate)
                    .Where(a => a.AcceptedDate != null)
                    .Select(a => a.AgreementVersion.AgreementType.IsOnBehalfOfAgreement() ? AgreementGroup.OnBehalfOf : AgreementGroup.RegulatedUser)
                    .FirstOrDefault(),
                CollegeLicenseNumber = enrollee.Certifications.First().LicenseNumber,
                PharmaNetId = enrollee.Certifications.First().PractitionerId,
            };
        }
    }
}
