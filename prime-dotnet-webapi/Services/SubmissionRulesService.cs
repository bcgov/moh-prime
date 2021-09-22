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

        public SubmissionRulesService(
            ApiDbContext context,
            ILogger<SubmissionRulesService> logger,
            IBusinessEventService businessEventService,
            ICollegeLicenceClient collegeLicenceClient)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _collegeLicenceClient = collegeLicenceClient;
        }

        /// <summary>
        /// All rules must pass for this enrollee to qualify to be automatically adjudicated.
        /// Failing rules will add Status Reasons to the current status.
        /// </summary>
        public async Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee)
        {
            var rules = new List<AutomaticAdjudicationRule>
            {
                new SelfDeclarationRule(),
                new AddressRule(),
                new VerifiedAddressRule(),
                new PharmanetValidationRule(_collegeLicenceClient, _businessEventService),
                // new DeviceProviderRule(),
                new LicenceClassRule(),
                new AlwaysManualRule(),
                new IdentityAssuranceLevelRule(),
                new IdentityProviderRule(),
                new NoAssignedAgreementRule(),
            };

            return await ProcessRules(rules, enrollee);
        }

        /// <summary>
        /// All rules must pass for an update to be considered minor enough to not warrant going through the (Auto) adjudication proccess.
        /// These rules will not alter the enrollee object.
        /// </summary>
        public async Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate)
        {
            var rules = new List<MinorUpdateRule>
            {
                new DateRule(),
                new CurrentToaRule(),
                new AllowableChangesRule(profileUpdate)
            };

            return await ProcessRules(rules, enrollee);
        }

        public async Task<bool> PassesPharmanetValidationRule(Enrollee enrollee)
        {
            var rules = new List<AutomaticAdjudicationRule>
            {
                new PharmanetValidationRule(_collegeLicenceClient, _businessEventService),
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
