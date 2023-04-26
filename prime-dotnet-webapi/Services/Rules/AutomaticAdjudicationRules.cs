using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.HttpClients;
using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.Services.Rules
{
    /// <summary>
    /// Automatic Adjudication Rules will add Status Reasons to the current status on a failure.
    /// </summary>
    public abstract class AutomaticAdjudicationRule : IEnrolleeRule
    {
        public abstract Task<bool> ProcessRule(Enrollee enrollee);
    }

    public class SelfDeclarationRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.SelfDeclarations.Any())
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.SelfDeclaration);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    // Check to see if any of the addresses are outside of BC
    public class AddressRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            var addresses = new Address[] { enrollee.PhysicalAddress, enrollee.MailingAddress, enrollee.VerifiedAddress }
                .Where(a => a != null);

            if (addresses.Any(a => !a.IsInBC))
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.Address);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    // Enrollees without a verified addresses from BCSC go to manual
    public class VerifiedAddressRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.VerifiedAddress == null)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.NoVerifiedAddress);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    // If enrollee has credentials, check to see if the college license is active in PharmaNet and matches the enrollee
    public class PharmanetValidationRule : AutomaticAdjudicationRule
    {
        private readonly ICollegeLicenceClient _collegeLicenceClient;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEnrolleeService _enrolleeService;

        public PharmanetValidationRule(
            ICollegeLicenceClient collegeLicenceClient,
            IBusinessEventService businessEventService,
            IEnrolleeService enrolleeService)
        {
            _collegeLicenceClient = collegeLicenceClient;
            _businessEventService = businessEventService;
            _enrolleeService = enrolleeService;
        }

        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            var certifications = MapCertifications(enrollee);

            bool passed = true;
            string testedPharmaNetIds;
            PharmanetCollegeRecord record;

            foreach (var cert in certifications)
            {
                record = null;
                try
                {
                    record = await _collegeLicenceClient.GetCollegeRecordAsync(cert.Prefix, cert.LicenseNumber);
                    testedPharmaNetIds = cert.ToString();
                }
                catch (PharmanetCollegeApiException)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PharmanetError, cert.ToString());
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.Prefix, cert.LicenseNumber, "An error occurred calling the PharmaNet API.");
                    passed = false;
                    continue;
                }

                //After validating with prescriber prefix and no hit, try to validate with non-prescriber prefix
                if (record == null)
                {
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.Prefix, cert.LicenseNumber, "Record not found in PharmaNet.");
                }
                else
                {
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.Prefix, cert.LicenseNumber,
                        $"A record was found in PharmaNet with effective date {record.EffectiveDate:dd MMM yyy} and status {record.Status}.");
                }

                //As long as the licence class has non prescribing prefix, fetch the college record
                if (cert.NonPrescribingPrefix != null)
                {
                    try
                    {
                        PharmanetCollegeRecord nonPrescribing = await _collegeLicenceClient.GetCollegeRecordAsync(cert.NonPrescribingPrefix, cert.LicenseNumber);
                        testedPharmaNetIds += $", {cert.NonPrescribingPrefix}-{cert.LicenseNumber}";
                        if (nonPrescribing != null)
                        {
                            await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.NonPrescribingPrefix, cert.LicenseNumber,
                                $"A record was found in PharmaNet with effective date {nonPrescribing.EffectiveDate:dd MMM yyy} and status {nonPrescribing.Status}.");

                            bool useNonPrescribing = false;

                            if (record != null)
                            {
                                if ((record.Status != "P" && nonPrescribing.Status == "P") ||
                                (record.Status == "P" && nonPrescribing.Status == "P" && nonPrescribing.EffectiveDate > record.EffectiveDate))
                                {
                                    //if non-prescrbing one is practicing but other is not or
                                    // both practicing and non-prescribing has the most recent effective date
                                    useNonPrescribing = true;
                                }
                            }
                            else
                            {
                                // prescribing does not exist
                                useNonPrescribing = true;
                            }

                            if (useNonPrescribing)
                            {
                                cert.Prefix = cert.NonPrescribingPrefix;
                                record = nonPrescribing;
                            }
                        }
                        else
                        {
                            await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.NonPrescribingPrefix, cert.LicenseNumber, "Record not found in Pharmanet.");
                        }
                    }
                    catch (PharmanetCollegeApiException)
                    {
                        enrollee.AddReasonToCurrentStatus(StatusReasonType.PharmanetError, $"{cert.NonPrescribingPrefix}-{cert.LicenseNumber}");
                        await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.NonPrescribingPrefix, cert.LicenseNumber, "An error occurred calling the Pharmanet API.");
                        passed = false;
                        continue;
                    }
                }

                if (record == null)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NotInPharmanet, testedPharmaNetIds);
                    passed = false;
                    continue;
                }
                else
                {
                    //save the prefix
                    await _enrolleeService.UpdateCertificationPrefix(cert.Id, cert.Prefix);
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.Prefix, cert.LicenseNumber, "College record stored in PRIME.");
                }

                if (!record.MatchesEnrolleeByName(enrollee))
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NameDiscrepancy, $"{cert} returned \"{record.FirstName} {record.LastName}\".");
                    passed = false;
                }
                if (record.DateofBirth.Date != enrollee.DateOfBirth.Date)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.BirthdateDiscrepancy, $"{cert} returned {record.DateofBirth:d MMM yyyy}");
                    passed = false;
                }
                if (record.Status != "P")
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.Practicing, cert.ToString());
                    passed = false;
                }
            }

            return passed;
        }

        private class CertificationDto
        {
            public int Id { get; set; }
            public string Prefix { get; set; }
            public string NonPrescribingPrefix { get; set; }
            public string LicenseNumber { get; set; }
            public override string ToString()
            {
                return $"{Prefix}-{LicenseNumber}";
            }
        }

        private static IEnumerable<CertificationDto> MapCertifications(Enrollee enrollee)
        {
            var filteredCertifications = enrollee.Certifications.Where(c =>
                c.License.CurrentLicenseDetail.Validate
                && !(c.License.CurrentLicenseDetail.PrescriberIdType == PrescriberIdType.Optional && c.PractitionerId == null)
            ).Select(c => new CertificationDto
            {
                Id = c.Id,
                Prefix = c.License.CurrentLicenseDetail.Prefix,
                NonPrescribingPrefix = c.License.CurrentLicenseDetail.NonPrescribingPrefix,
                LicenseNumber = c.License.CurrentLicenseDetail.PrescriberIdType.HasValue
                    ? c.PractitionerId
                    : c.LicenseNumber,
            });

            // If enrollee choses device provider, add device provider as a licence and check it against PharmaNet
            if (!string.IsNullOrWhiteSpace(enrollee.DeviceProviderIdentifier))
            {
                filteredCertifications = filteredCertifications.Append(new CertificationDto
                {
                    // ATTN: the prefix below is a placeholder
                    Prefix = "P1",
                    LicenseNumber = enrollee.DeviceProviderIdentifier
                });
            }

            return filteredCertifications;
        }
    }

    public class DeviceProviderRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.HasCareSetting(CareSettingType.DeviceProvider) || enrollee.DeviceProviderIdentifier != null)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.DeviceProvider);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    public class LicenceClassRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            // Passes rule if enrollee.Certifications is null or empty
            foreach (var cert in enrollee.Certifications ?? Enumerable.Empty<Certification>())
            {
                if (cert.License.CurrentLicenseDetail.Manual)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.LicenceClass);
                    return Task.FromResult(false);
                }
            }

            return Task.FromResult(true);
        }
    }

    public class AlwaysManualRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.AlwaysManual)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.AlwaysManual);
            }

            return Task.FromResult(!enrollee.AlwaysManual);
        }
    }

    public class NoAssignedAgreementRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            var newestAssignedAgreement = enrollee.Submissions
                .OrderByDescending(s => s.CreatedDate)
                .Select(s => s.AgreementType)
                .First();

            if (newestAssignedAgreement == null)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.NoAssignedAgreement);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    public class IdentityAssuranceLevelRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.IdentityAssuranceLevel < 3)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.AssuranceLevel);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    public class IdentityProviderRule : AutomaticAdjudicationRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.IdentityProvider != AuthConstants.BCServicesCard)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.IdentityProvider, $"Method used: {enrollee.IdentityProvider}");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    public class IsPotentialPaperEnrolleeReturnee : AutomaticAdjudicationRule
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IEnrolleePaperSubmissionService _enrolleePaperSubmissionService;

        public IsPotentialPaperEnrolleeReturnee(
            IBusinessEventService businessEventService,
            IEnrolleePaperSubmissionService enrolleePaperSubmissionService)
        {
            _businessEventService = businessEventService;
            _enrolleePaperSubmissionService = enrolleePaperSubmissionService;
        }
        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            var paperEnrollees = await _enrolleePaperSubmissionService.GetPotentialPaperEnrolleeReturneesAsync(enrollee.DateOfBirth);

            // If approved, linked, or there is no match then we don't need to worry about this rule
            if (enrollee.ApprovedDate.HasValue
                || !paperEnrollees.Any()
                || await _enrolleePaperSubmissionService.IsEnrolleeLinkedAsync(enrollee.Id))
            {
                return true;
            }

            var potentialPaperEnrolleeGpid = await _enrolleePaperSubmissionService.GetLinkedGpidAsync(enrollee.Id);
            var paperEnrolleeMatchId = -1;
            var paperEnrolleeIdsAsString = string.Join(", ", paperEnrollees.Select(e => e.Id));

            // if there's a match and GPID is provided
            if (potentialPaperEnrolleeGpid != null)
            {
                // Check if GPID match one of the paper enrolment
                foreach (var PaperEnrollee in paperEnrollees)
                {
                    if (PaperEnrollee.GPID == potentialPaperEnrolleeGpid)
                    {
                        paperEnrolleeMatchId = PaperEnrollee.Id;
                    }
                }

                if (paperEnrolleeMatchId == -1)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PaperEnrolmentMismatch, $"User-Provided GPID: {potentialPaperEnrolleeGpid}");
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"Birthdate matches enrolment(s): {paperEnrolleeIdsAsString}");
                    return false;
                }
                // if a match is found, link to paper enrolment and confirm the linkage here, if failed to link we add status reason.
                if (!await _enrolleePaperSubmissionService.LinkEnrolleeToPaperEnrolmentAsync(enrolleeId: enrollee.Id, paperEnrolleeId: paperEnrolleeMatchId))
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.UnableToLinkToPaperEnrolment, $"User-Provided GPID: {potentialPaperEnrolleeGpid}");
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"Birthdate matches enrolment(s): {paperEnrolleeIdsAsString}");
                    return false;
                }

                // First enrolment: check if the related paper enrolment is flagged for AlwaysManual
                // If so, link enrolments and mark BCSC enrolment as AlwaysManual too and send to manual enrolment
                if (paperEnrollees.Any(pe => pe.Id == paperEnrolleeMatchId && pe.AlwaysManual))
                {
                    enrollee.AlwaysManual = true;
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.AlwaysManual);
                    return false;
                }

                await _businessEventService.CreatePaperEnrolmentLinkEventAsync(enrollee.Id, "Paper enrolment has been linked");

                return true;
            }
            // if yes and GPID not provided - flag with "Possible match with paper enrolment"
            else
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"Birthdate matches enrolment(s): {paperEnrolleeIdsAsString}");
                return false;
            }
        }
    }
}
