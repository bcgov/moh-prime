using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using Prime.Models;
using Prime.ViewModels;
using Prime.Services.Rules;
using Prime.HttpClients;

namespace Prime.Services
{
    public class SubmissionRulesService : BaseService, ISubmissionRulesService
    {
        private readonly ICollegeLicenceClient _collegeLicenceClient;
        private readonly IBusinessEventService _businessEventService;

        public SubmissionRulesService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ICollegeLicenceClient collegeLicenceClient,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _collegeLicenceClient = collegeLicenceClient;
            _businessEventService = businessEventService;
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
