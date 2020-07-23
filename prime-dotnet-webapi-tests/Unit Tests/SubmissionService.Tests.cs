using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using Microsoft.Extensions.Logging;

namespace PrimeTests.UnitTests
{
    public class SubmissionServiceTests : InMemoryDbTest
    {
        public SubmissionService CreateService(
            IHttpContextAccessor httpContext = null,
            IAccessTermService accessTermService = null,
            ISubmissionRulesService submissionRulesService = null,
            IBusinessEventService businessEventService = null,
            IEmailService emailService = null,
            IEnrolleeService enrolleeService = null,
            IEnrolleeProfileVersionService enrolleeProfileVersionService = null,
            IVerifiableCredentialService verifiableCredentialService = null,
            IPrivilegeService privilegeService = null,
            ILogger<SubmissionService> logger = null)
        {
            return new SubmissionService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                accessTermService ?? A.Fake<IAccessTermService>(),
                submissionRulesService ?? A.Fake<ISubmissionRulesService>(),
                businessEventService ?? A.Fake<IBusinessEventService>(),
                emailService ?? A.Fake<IEmailService>(),
                enrolleeService ?? A.Fake<IEnrolleeService>(),
                enrolleeProfileVersionService ?? A.Fake<IEnrolleeProfileVersionService>(),
                verifiableCredentialService ?? A.Fake<IVerifiableCredentialService>(),
                privilegeService ?? A.Fake<IPrivilegeService>(),
                logger ?? A.Fake<ILogger<SubmissionService>>()
            );
        }
    }
}

//     public interface ISubmissionService
//     {
//         Task SubmitApplicationAsync(int enrolleeId, EnrolleeProfileViewModel enrolleProfile);

//         Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin);

//         Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
//     }
