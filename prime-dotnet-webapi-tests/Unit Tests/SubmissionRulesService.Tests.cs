using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Services
{
    public class SubmissionRulesServiceTests2
    {
        public static SubmissionRulesService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null,
            IPharmanetApiService pharmanetApiService = null,
            IAccessTermService accessTermService = null)
        {
            return new SubmissionRulesService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                pharmanetApiService ?? A.Fake<IPharmanetApiService>(),
                accessTermService ?? A.Fake<IAccessTermService>()
            );
        }
    }
}

//     public interface ISubmissionRulesService
//     {
//         Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee);

//         Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeProfileViewModel profileUpdate);
//     }
