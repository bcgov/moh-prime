using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class AccessTermServiceTests : InMemoryDbTest
    {
        public AccessTermService CreateService(
            IHttpContextAccessor httpContext = null,
            IRazorConverterService razorConverterService = null)
        {
            return new AccessTermService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                razorConverterService ?? A.Fake<IRazorConverterService>()
            );
        }
    }
}

// public interface IAccessTermService
// {

//     Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId);

//     Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId);

//     Task<AccessTerm> GetEnrolleesAccessTermAsync(int enrolleeId, int accessTermId);

//     Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId, int year);

//     Task CreateEnrolleeAccessTermAsync(Enrollee enrollee);

//     Task AcceptCurrentAccessTermAsync(Enrollee enrollee);

//     Task ExpireCurrentAccessTermAsync(Enrollee enrollee);

//     Task<bool> AccessTermExistsOnEnrolleeAsync(int accessTermId, int enrolleeId);

//     Task<bool> IsCurrentByEnrolleeAsync(Enrollee enrollee);
// }
