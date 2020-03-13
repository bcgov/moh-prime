using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using Prime.Models;
using Prime.Services.Rules;

namespace Prime.Services
{
    public class SubmissionRulesService : BaseService, ISubmissionRulesService
    {
        private readonly IPharmanetApiService _pharmanetApiService;

        public SubmissionRulesService(
            ApiDbContext context, IHttpContextAccessor httpContext, IPharmanetApiService pharmanetApiService)
            : base(context, httpContext)
        {
            _pharmanetApiService = pharmanetApiService;
        }

        /// <summary>
        /// All rules must pass for this enrollee to qualify to be automatically adjudicated.
        /// Failing rules will add Status Reasons to the current status.
        /// </summary>
        public async Task<bool> QualifiesForAutomaticAdjudication(Enrollee enrollee)
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
