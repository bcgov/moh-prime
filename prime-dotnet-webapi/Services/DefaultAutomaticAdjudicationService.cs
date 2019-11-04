using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultAutomaticAdjudicationServiceService : BaseService, IAutomaticAdjudicationService
    {
        readonly List<IAutomaticAdjudicationRule> _rules = new List<IAutomaticAdjudicationRule>();

        public DefaultAutomaticAdjudicationServiceService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {
            // add the rules to the list that this implementation will process
            _rules.Add(new SelfDeclarationRule());
            _rules.Add(new AddressRule());
            _rules.Add(new PumpProviderRule());
            _rules.Add(new CertificationNameRule());
            _rules.Add(new LicenceClassRule());
            _rules.Add(new LicenceNumberRule());
        }

        public bool QualifiesForAutomaticAdjudication(Enrolment enrolment)
        {
            // Check all of the rules to see if this should qualify to be automatically adjudicated.
            // If it does not qualify not, it should add the reasons it did not meet the criteria to the current enrolment status
            bool passed = true;
            foreach (var rule in _rules)
            {
                passed &= rule.ProcessRule(enrolment);
            }

            return passed;
        }

        private interface IAutomaticAdjudicationRule
        {
            bool ProcessRule(Enrolment enrolment);
        }

        private abstract class BaseAutomaticAdjudicationRule : IAutomaticAdjudicationRule
        {
            public bool ProcessRule(Enrolment enrolment)
            {
                // make sure that there is an enrolment to process the rule against
                if (enrolment == null)
                {
                    throw new ArgumentNullException($"Could not process enrolment rule, passed in Enrolment cannot be null.");
                }

                // process the rule and check the results
                var results = this.ProcessRuleInternal(enrolment);

                bool ruleFailed = results.Any();

                if (ruleFailed)
                {
                    // get the current status record for the enrolment
                    var currentStatus = enrolment.CurrentStatus;

                    // make sure there is a current status
                    if (currentStatus == null)
                    {
                        throw new ArgumentNullException($"Could not process enrolment rule, current status was missing for Enrolment.Id={enrolment.Id}.");
                    }

                    // for every item returned in the results, add the reason to the current status
                    foreach (var item in results)
                    {
                        if (currentStatus.EnrolmentStatusReasons == null)
                        {
                            currentStatus.EnrolmentStatusReasons = new List<EnrolmentStatusReason>(0);
                        }
                        currentStatus.EnrolmentStatusReasons.Add(new EnrolmentStatusReason { EnrolmentStatus = currentStatus, StatusReasonCode = item.Code });
                    }
                }

                return !ruleFailed;
            }

            public abstract ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment);

        }

        /// check to see if any of the self-declaration rules were answered as 'Yes'
        private class SelfDeclarationRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if any of the self-declaration rules were answered as 'Yes'
                // note: if for some reason the question was not answered, we will assume 'Yes'
                if ((enrolment.HasConviction ?? true)
                        | (enrolment.HasDisciplinaryAction ?? true)
                        | (enrolment.HasPharmaNetSuspended ?? true)
                        | (enrolment.HasRegistrationSuspended ?? true))
                {
                    result.Add(new StatusReason { Code = StatusReason.SELF_DECLARATION_CODE });
                }

                return result;
            }
        }

        /// check to see if any of the addresses are outside of BC
        private class AddressRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if any of the addresses are outside of BC
                var physicalAddress = enrolment.Enrollee?.PhysicalAddress;
                var mailingAddress = enrolment.Enrollee?.MailingAddress;
                if ((physicalAddress != null
                        && physicalAddress.ProvinceCode != null
                        && !physicalAddress.ProvinceCode.Equals(Province.BRITISH_COLUMBIA_CODE))
                    || (mailingAddress != null
                        && mailingAddress.ProvinceCode != null
                        && !mailingAddress.ProvinceCode.Equals(Province.BRITISH_COLUMBIA_CODE)))
                {
                    result.Add(new StatusReason { Code = StatusReason.ADDRESS_CODE });
                }

                return result;
            }
        }

        /// check to see if the enrolment is a pump provider
        private class PumpProviderRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if the enrolment is a pump provider
                // note: if for some reason the question was not answered, we will assume 'Yes'
                if ((enrolment.IsDeviceProvider ?? true)
                        & (enrolment.IsInsulinPumpProvider ?? true))
                {
                    result.Add(new StatusReason { Code = StatusReason.PUMP_PROVIDER_CODE });
                }

                return result;
            }
        }

        /// check to see if the enrolment has a particular licence class
        private class LicenceClassRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if the enrolment has a particular licence class
                if (enrolment.Certifications != null
                        && enrolment.Certifications.Any())
                {
                    // TODO - properly implement this check
                    // foreach (var item in enrolment.Certifications)
                    // {
                    //     if (item.LicenseCode.Equals(XX))
                    //     {
                    result.Add(new StatusReason { Code = StatusReason.LICENCE_CLASS_CODE });
                    //         break;
                    //     }
                    // }
                }

                return result;
            }
        }

        /// check to see if the enrolment has an inactive license number
        private class LicenceNumberRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if the enrolment has an inactive license number
                if (enrolment.Certifications != null
                        && enrolment.Certifications.Any())
                {
                    foreach (var item in enrolment.Certifications)
                    {
                        // TODO - properly implement this check
                        if (item.LicenseNumber != null)
                        {
                            result.Add(new StatusReason { Code = StatusReason.NOT_IN_PHARMANET_CODE });
                            break;
                        }
                    }

                }

                return result;
            }
        }

        /// check to see if the enrolment has an certification name discrepancy
        private class CertificationNameRule : BaseAutomaticAdjudicationRule
        {
            public override ICollection<StatusReason> ProcessRuleInternal(Enrolment enrolment)
            {
                var result = new List<StatusReason>(0);
                // check to see if the enrolment has an certification name discrepancy
                if (enrolment.HasCertification ?? false)
                {
                    // TODO - properly implement this check
                    result.Add(new StatusReason { Code = StatusReason.NAME_DISCREPANCY_CODE });
                }

                return result;
            }
        }
    }
}
