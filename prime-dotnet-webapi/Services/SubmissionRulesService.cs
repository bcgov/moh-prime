using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.HttpClients;
using Prime.Models;
using Prime.Services.Rules;
using Prime.ViewModels;

namespace Prime.Services
{
    public class SubmissionRulesService : BaseService, ISubmissionRulesService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly ICollegeLicenceClient _collegeLicenceClient;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolleePaperSubmissionService _enrolleePaperSubmissionService;
        private readonly IDeviceProviderService _deviceProviderService;

        public SubmissionRulesService(
            ApiDbContext context,
            ILogger<SubmissionRulesService> logger,
            IBusinessEventService businessEventService,
            ICollegeLicenceClient collegeLicenceClient,
            IEnrolleeService enrolleeService,
            IEnrolleePaperSubmissionService enrolleePaperSubmissionService,
            IDeviceProviderService deviceProviderService)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _collegeLicenceClient = collegeLicenceClient;
            _enrolleeService = enrolleeService;
            _enrolleePaperSubmissionService = enrolleePaperSubmissionService;
            _deviceProviderService = deviceProviderService;

            _logger.LogDebug($"Going to use {_collegeLicenceClient.GetType().Name} in PharmanetValidationRule");
        }

        /// <summary>
        /// All rules must pass for this enrollee to qualify to be automatically adjudicated.
        /// Failing rules will add Status Reasons to the current status.
        /// </summary>
        public async Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee, bool ignoreDOBDiscrepancy = false)
        {
            var rules = new List<AutomaticAdjudicationRule>
            {
                new SelfDeclarationRule(),
                new AddressRule(),
                new VerifiedAddressRule(),
                new PharmanetValidationRule(_collegeLicenceClient, _businessEventService, _enrolleeService, ignoreDOBDiscrepancy),
                new DeviceProviderRule(_deviceProviderService),
                new LicenceClassRule(),
                new AlwaysManualRule(),
                new IdentityAssuranceLevelRule(),
                new IdentityProviderRule(),
                new NoAssignedAgreementRule(),
                new IsPotentialPaperEnrolleeReturnee(_businessEventService, _enrolleePaperSubmissionService),
                new UnlistedCertificationRule(),
                new PossibleDuplicateRule(_enrolleeService)
            };

            return await ProcessRules(rules, enrollee);
        }

        /// <summary>
        /// All rules must pass for an update to be considered minor enough to not warrant going through the (Auto) adjudication proccess.
        /// These rules will not alter the enrollee object.
        /// </summary>
        public async Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate, List<int> newestAgreementVersionIds)
        {
            var rules = new List<MinorUpdateRule>
            {
                new DateRule(),
                new CurrentToaRule(newestAgreementVersionIds),
                new CorrectToaRule(),
                new AllowableChangesRule(profileUpdate)
            };

            return await ProcessRules(rules, enrollee);
        }

        private async Task<bool> ProcessRules(IEnumerable<IEnrolleeRule> rules, Enrollee enrollee)
        {
            bool passed = true;
            foreach (var rule in rules)
            {
                passed &= await rule.ProcessRule(enrollee);
            }

            return passed;
        }
    }
}
