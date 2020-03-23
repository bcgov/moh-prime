using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using Prime.Models;
using Prime.ViewModels;
using Prime.Services.Rules;

namespace Prime.Services
{
    public class SubmissionRulesService : BaseService, ISubmissionRulesService
    {
        private readonly IPharmanetApiService _pharmanetApiService;
        private readonly IAccessTermService _accessTermService;

        public SubmissionRulesService(
            ApiDbContext context, IHttpContextAccessor httpContext,
            IPharmanetApiService pharmanetApiService,
            IAccessTermService accessTermService)
            : base(context, httpContext)
        {
            _pharmanetApiService = pharmanetApiService;
            _accessTermService = accessTermService;
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
                new PharmanetValidationRule(_pharmanetApiService),
                // TODO removed until after Community Practice
                // new DeviceProviderRule(),
                new LicenceClassRule(),
                new AlwaysManualRule()
            };

            return await ProcessRules(rules, enrollee);
        }

        /// <summary>
        /// All rules must pass for an update to be considered minor enough to not warrant going through the (Auto) adjudication proccess.
        /// These rules will not alter the enrollee object.
        /// </summary>
        public async Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeProfileViewModel profileUpdate)
        {
            var rules = new List<MinorUpdateRule>
            {
                //new DateRule(),
                new CurrentToaRule(_accessTermService),
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
