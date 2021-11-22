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

        public PharmanetValidationRule(
            ICollegeLicenceClient collegeLicenceClient,
            IBusinessEventService businessEventService)
        {
            _collegeLicenceClient = collegeLicenceClient;
            _businessEventService = businessEventService;
        }

        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            var certifications = enrollee.Certifications.Where(c => c.License.Validate);

            // If enrollee choses device provider, add device provider as a licence and check it against PharmaNet
            if (!string.IsNullOrWhiteSpace(enrollee.DeviceProviderIdentifier))
            {
                certifications = certifications.Append(new Certification
                {
                    // ATTN: the prefix below is a placeholder
                    License = new License { Prefix = "P1" },
                    LicenseNumber = enrollee.DeviceProviderIdentifier
                });
            }

            bool passed = true;

            foreach (var cert in certifications)
            {
                if (cert.License.PrescriberIdType == PrescriberIdType.Optional && cert.PractitionerId == null)
                {
                    continue;
                }

                var licenceNumber = cert.License.PrescriberIdType.HasValue
                    ? cert.PractitionerId
                    : cert.LicenseNumber;

                var licenceText = $"{cert.License.Prefix}-{licenceNumber}";

                PharmanetCollegeRecord record = null;
                try
                {
                    record = await _collegeLicenceClient.GetCollegeRecordAsync(cert.License.Prefix, licenceNumber);
                }
                catch (PharmanetCollegeApiException)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PharmanetError, licenceText);
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.License.Prefix, licenceNumber, "An error occurred calling the Pharmanet API.");
                    passed = false;
                    continue;
                }
                if (record == null)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NotInPharmanet, licenceText);
                    await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.License.Prefix, licenceNumber, "Record not found in Pharmanet.");
                    passed = false;
                    continue;
                }
                await _businessEventService.CreatePharmanetApiCallEventAsync(enrollee.Id, cert.License.Prefix, licenceNumber, "A record was found in Pharmanet.");

                if (!record.MatchesEnrolleeByName(enrollee))
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NameDiscrepancy, $"{licenceText} returned \"{record.FirstName} {record.LastName}\".");
                    passed = false;
                }
                if (record.DateofBirth.Date != enrollee.DateOfBirth.Date)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.BirthdateDiscrepancy, $"{licenceText} returned {record.DateofBirth:d MMM yyyy}");
                    passed = false;
                }
                if (record.Status != "P")
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.Practicing, licenceText);
                    passed = false;
                }
            }

            return passed;
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
                if (cert.License.Manual)
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
        private readonly IEnrolleePaperSubmissionService _enrolleePaperSubmissionService;

        public IsPotentialPaperEnrolleeReturnee(IEnrolleePaperSubmissionService enrolleePaperSubmissionService)
        {
            _enrolleePaperSubmissionService = enrolleePaperSubmissionService;
        }
        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            var paperEnrollees = await _enrolleePaperSubmissionService.GetPotentialPaperEnrolleeReturneesAsync(enrollee.DateOfBirth);
            var potentialPaperEnrolleeGpid = await _enrolleePaperSubmissionService.GetLinkedGpidAsync(enrollee.Id);
            var paperEnrolleeMatchId = -1;
            var paperEnrolleeIdsAsString = CreateIdsString(paperEnrollees);

            // Check if there's a match on a birthdate in paper enrollees, get all the ones that have a match

            // If there is no match then we don't need to worry about this rule
            if (!paperEnrollees.Any())
            {
                return true;
            }

            // if yes and GPID is provided
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
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"birthdate matches enrolment: {paperEnrolleeIdsAsString}");
                    return false;
                }
                // if a match is found, link to paper enrolment and confirm the linkage here, if failed to link we add status reason.
                if (!await _enrolleePaperSubmissionService.LinkEnrolleeToPaperEnrolmentAsync(enrolleeId: enrollee.Id, paperEnrolleeId: paperEnrolleeMatchId))
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.UnableToLinkToPaperEnrolment, $"User-Provided GPID: {potentialPaperEnrolleeGpid}");
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"birthdate matches enrolment: {paperEnrolleeMatchId}");
                    return false;
                }
                return true;
            }
            // if yes and GPID not provided - flag with "Possible match with paper enrolment"
            else
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch);
                enrollee.AddReasonToCurrentStatus(StatusReasonType.PossiblePaperEnrolmentMatch, $"birthdate matches enrolment: {paperEnrolleeIdsAsString}");
                return false;
            }
        }

        static private string CreateIdsString(IEnumerable<Enrollee> paperEnrollees)
        {
            var paperEnrolleeIdsAsString = "";

            for (var i = 0; i < paperEnrollees.Count(); ++i)
            {
                if (i > 0)
                {
                    paperEnrolleeIdsAsString += ", ";
                }

                paperEnrolleeIdsAsString += paperEnrollees.ElementAt(i).Id;

                if (i == (paperEnrollees.Count() - 1))
                {
                    paperEnrolleeIdsAsString += ".";
                }
            }

            return paperEnrolleeIdsAsString;
        }
    }
}
