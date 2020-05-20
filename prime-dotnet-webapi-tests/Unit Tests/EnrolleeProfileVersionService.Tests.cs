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
    public class EnrolleeProfileVersionServiceTests : InMemoryDbTest
    {
        public EnrolleeProfileVersionService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new EnrolleeProfileVersionService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IEnrolleeProfileVersionService
//     {
//         Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId);

//         Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileVersionId);

//         Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionBeforeDateAsync(int enrolleeId, DateTimeOffset dateTime);

//         Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee);
//     }
