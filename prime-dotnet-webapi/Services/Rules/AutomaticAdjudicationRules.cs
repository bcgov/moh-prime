using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.HttpClients;

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
            if (!enrollee.PhysicalAddress.IsInBC
                || enrollee.MailingAddress?.IsInBC == false)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.Address);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    // If enrollee has credentials, check to see if the college license is active in PharmaNet and matches the enrollee
    public class PharmanetValidationRule : AutomaticAdjudicationRule
    {
        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public PharmanetValidationRule(ICollegeLicenceClient collegeLicenceClient)
        {
            _collegeLicenceClient = collegeLicenceClient;
        }

        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.Certifications == null || !enrollee.Certifications.Any())
            {
                // No certs to verify
                return true;
            }

            bool passed = true;

            foreach (var cert in enrollee.Certifications.Where(c => c.License.Validate))
            {
                PharmanetCollegeRecord record = null;
                try
                {
                    record = await _collegeLicenceClient.GetCollegeRecordAsync(cert);
                }
                catch (PharmanetCollegeApiException)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.PharmanetError, $"{cert.FullLicenseNumber}");
                    passed = false;
                    continue;
                }
                if (record == null)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NotInPharmanet, $"{cert.FullLicenseNumber}");
                    passed = false;
                    continue;
                }

                if (!record.MatchesEnrolleeByName(enrollee))
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.NameDiscrepancy, $"{cert.FullLicenseNumber} returned \"{record.FirstName} {record.LastName}\".");
                    passed = false;
                }
                if (record.DateofBirth.Date != enrollee.DateOfBirth.Date)
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.BirthdateDiscrepancy, $"{cert.FullLicenseNumber} returned {record.DateofBirth:d MMM yyyy}");
                    passed = false;
                }
                if (record.Status != "P")
                {
                    enrollee.AddReasonToCurrentStatus(StatusReasonType.Practicing, $"{cert.FullLicenseNumber}");
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
            if (!string.IsNullOrWhiteSpace(enrollee.DeviceProviderNumber)
                || enrollee.IsInsulinPumpProvider.GetValueOrDefault(true))
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.PumpProvider);
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
            if (enrollee.IdentityProvider != Auth.AuthConstants.BCServicesCard)
            {
                enrollee.AddReasonToCurrentStatus(StatusReasonType.IdentityProvider, $"Method used: {enrollee.IdentityProvider}");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
