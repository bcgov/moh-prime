using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Prime.Models;

// Strategy Pattern

namespace Prime.Services
{
    public class DefaultAutomaticAdjudicationService : BaseService, IAutomaticAdjudicationService
    {
        private readonly List<IAutomaticAdjudicationRule> _rules;

        public DefaultAutomaticAdjudicationService(
            ApiDbContext context, IHttpContextAccessor httpContext, IPharmanetApiService pharmanetApiService)
            : base(context, httpContext)
        {
            _rules = new List<IAutomaticAdjudicationRule>
            {
                new SelfDeclarationRule(),
                new AddressRule(),
                new PharmanetValidationRule(pharmanetApiService),
                new DeviceProviderRule()
            };
        }

        public async Task<bool> QualifiesForAutomaticAdjudication(Enrollee enrollee)
        {
            // All rules must pass for this enrollee to qualify to be automatically adjudicated.
            // Failing rules will add Status Reasons to the current status.
            bool passed = true;
            foreach (var rule in _rules)
            {
                passed &= await rule.ProcessRule(enrollee);
            }

            return passed;
        }

        public interface IAutomaticAdjudicationRule
        {
            Task<bool> ProcessRule(Enrollee enrollee);
        }

        public abstract class BaseAutomaticAdjudicationRule : IAutomaticAdjudicationRule
        {
            public async Task<bool> ProcessRule(Enrollee enrollee)
            {
                if (enrollee == null)
                {
                    throw new ArgumentNullException(nameof(enrollee));
                }

                return await ProcessRuleInternal(enrollee);
            }

            protected void AddReason(Enrollee enrollee, short statusReasonCode, string statusReasonNote = null)
            {
                var currentStatus = enrollee.CurrentStatus;
                if (currentStatus == null)
                {
                    throw new InvalidOperationException($"Could not add Status Reason for Enrollee with UserId \"{enrollee.UserId}\", Current Status is invalid.");
                }

                currentStatus.AddStatusReason(statusReasonCode, statusReasonNote);
            }

            protected abstract Task<bool> ProcessRuleInternal(Enrollee enrollee);
        }

        // check to see if any of the self-declaration rules were answered as 'Yes'
        public class SelfDeclarationRule : BaseAutomaticAdjudicationRule
        {
            protected override Task<bool> ProcessRuleInternal(Enrollee enrollee)
            {
                // check to see if any of the self-declaration rules were answered as 'Yes'
                // note: if for some reason the question was not answered, we will assume 'Yes'
                if (enrollee.HasConviction.GetValueOrDefault(true)
                    || enrollee.HasDisciplinaryAction.GetValueOrDefault(true)
                    || enrollee.HasPharmaNetSuspended.GetValueOrDefault(true)
                    || enrollee.HasRegistrationSuspended.GetValueOrDefault(true))
                {
                    AddReason(enrollee, StatusReason.SELF_DECLARATION_CODE);
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
        }

        // check to see if any of the addresses are outside of BC
        public class AddressRule : BaseAutomaticAdjudicationRule
        {
            protected override Task<bool> ProcessRuleInternal(Enrollee enrollee)
            {
                // check to see if any of the addresses are outside of BC
                var provinceCodes = new[] { enrollee.PhysicalAddress?.ProvinceCode, enrollee.MailingAddress?.ProvinceCode };
                if (provinceCodes.Any(p => p != null
                    && !p.Equals(Province.BRITISH_COLUMBIA_CODE, StringComparison.OrdinalIgnoreCase)))
                {
                    AddReason(enrollee, StatusReason.ADDRESS_CODE);
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
        }

        // If enrollee has credentials, check to see if the college license is active in PharmaNet and matches the enrollee
        public class PharmanetValidationRule : BaseAutomaticAdjudicationRule
        {
            private readonly IPharmanetApiService _pharmanetApiService;

            public PharmanetValidationRule(IPharmanetApiService pharmanetApiService)
            {
                _pharmanetApiService = pharmanetApiService;
            }

            protected override async Task<bool> ProcessRuleInternal(Enrollee enrollee)
            {
                if (enrollee.Certifications?.Any() != true)
                {
                    // No certs to verify
                    return true;
                }

                bool passed = true;

                foreach (var cert in enrollee.Certifications)
                {
                    PharmanetCollegeRecord record = null;
                    try
                    {
                        record = await _pharmanetApiService.GetCollegeRecordAsync(cert);
                    }
                    catch (DefaultPharmanetApiService.PharmanetCollegeApiException)
                    {
                        AddReason(enrollee, StatusReason.PHARMANET_ERROR_CODE, $"{cert.FullLicenseNumber}");
                        passed = false;
                        continue;
                    }
                    if (record == null)
                    {
                        AddReason(enrollee, StatusReason.NOT_IN_PHARMANET_CODE, $"{cert.FullLicenseNumber}");
                        passed = false;
                        continue;
                    }

                    if (!record.MatchesEnrolleeByName(enrollee))
                    {
                        AddReason(enrollee, StatusReason.NAME_DISCREPANCY_CODE, $"{cert.FullLicenseNumber} returned \"{record.firstName} {record.lastName}\".");
                        passed = false;
                    }
                    if (record.dateofBirth.Date != enrollee.DateOfBirth.Date)
                    {
                        AddReason(enrollee, StatusReason.BIRTHDATE_DISCREPANCY_CODE, $"{cert.FullLicenseNumber} returned {record.dateofBirth.ToString("d MMM yyyy")}");
                        passed = false;
                    }
                    if (record.status != "P")
                    {
                        AddReason(enrollee, StatusReason.PRACTICING_CODE, $"{cert.FullLicenseNumber}");
                        passed = false;
                    }
                }

                return passed;
            }
        }

        // Check to see if the enrollee is a device or pump provider
        public class DeviceProviderRule : BaseAutomaticAdjudicationRule
        {
            protected override Task<bool> ProcessRuleInternal(Enrollee enrollee)
            {
                if (!string.IsNullOrWhiteSpace(enrollee.DeviceProviderNumber)
                    || enrollee.IsInsulinPumpProvider.GetValueOrDefault(true))
                {
                    AddReason(enrollee, StatusReason.PUMP_PROVIDER_CODE);
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
        }

        // If the enrollee has licence classes, validate them
        public class LicenceClassRule : BaseAutomaticAdjudicationRule
        {
            protected override Task<bool> ProcessRuleInternal(Enrollee enrollee)
            {
                var passed = true;
                if (enrollee.Certifications?.Any() == true)
                {
                    // TODO properly implement this check
                    foreach (var item in enrollee.Certifications)
                    {
                        if (item.LicenseCode > 0)
                        {
                            AddReason(enrollee, StatusReason.LICENCE_CLASS_CODE);
                            passed = false;
                            break;
                        }
                    }
                }

                return Task.FromResult(passed);
            }
        }
    }
}
