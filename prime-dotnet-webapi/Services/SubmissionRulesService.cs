using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using Prime.Models;
using Prime.ViewModels;
using Prime.Services.Rules;
using Prime.Services.Clients;

namespace Prime.Services
{
    public class SubmissionRulesService : BaseService, ISubmissionRulesService
    {
        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public SubmissionRulesService(
            ApiDbContext context, IHttpContextAccessor httpContext,
            ICollegeLicenceClient collegeLicenceClient)
            : base(context, httpContext)
        {
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
                new PharmanetValidationRule(_collegeLicenceClient),
                // TODO removed until after Community Practice
                // new DeviceProviderRule(),
                new LicenceClassRule(),
                new AlwaysManualRule(),
                new IdentityAssuranceLevelRule(),
                new IdentityProviderRule(),
                new RequestingRemoteAccessRule()
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
