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
    public class EnrolleeSubmissionServiceTests : InMemoryDbTest
    {
        public EnrolleeSubmissionService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new EnrolleeSubmissionService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}
