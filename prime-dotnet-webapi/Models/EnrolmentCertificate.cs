using System;
using System.Linq;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

using Prime.Models.Api;

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
        public IEnumerable<EnrolleeCertDto> Licences { get; set; }
        public string AccessType { get; set; }
        public string DeviceProviderId { get; set; }
        public IEnumerable<HealthAuthority> HealthAuthories { get; set; }


        public static EnrolmentCertificate Create(EnrolmentCertificateAccessToken token, Enrollee enrollee)
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
                CareSettings = token.CareSettingCode == null ?
                    enrollee.EnrolleeCareSettings.Select(ecs => ecs.CareSetting) :
                    enrollee.EnrolleeCareSettings.Where(ecs => ecs.CareSettingCode == token.CareSettingCode).Select(ecs => ecs.CareSetting),
                HealthAuthories = token.HealthAuthorityCode == null ?
                    enrollee.EnrolleeHealthAuthorities.Select(e => e.HealthAuthority) :
                    enrollee.EnrolleeHealthAuthorities.Where(eha => (int)eha.HealthAuthorityCode == token.HealthAuthorityCode).Select(eha => eha.HealthAuthority),
                Group = enrollee.Agreements.OrderByDescending(a => a.CreatedDate)
                    .Where(a => a.AcceptedDate != null)
                    .Select(a => a.AgreementVersion.AgreementType.IsOnBehalfOfAgreement() ? AgreementGroup.OnBehalfOf : AgreementGroup.RegulatedUser)
                    .FirstOrDefault(),
                Licences = enrollee.Certifications.Select(cert =>
                    new EnrolleeCertExtDto
                    {
                        PractRefId = cert.Prefix ?? cert.License.CurrentLicenseDetail.Prefix,
                        CollegeLicenceNumber = cert.LicenseNumber,
                        PharmaNetId = cert.PractitionerId,
                        CollegeCode = cert.CollegeCode,
                    }),
                AccessType = enrollee.Agreements.OrderByDescending(a => a.CreatedDate)
                    .Where(a => a.AcceptedDate != null)
                    .Select(a => a.AgreementVersion.AccessType)
                    .FirstOrDefault(),
                DeviceProviderId = enrollee.EnrolleeDeviceProviders
                    .Select(dp => dp.DeviceProviderId)
                    .FirstOrDefault()
            };
        }
    }
}
