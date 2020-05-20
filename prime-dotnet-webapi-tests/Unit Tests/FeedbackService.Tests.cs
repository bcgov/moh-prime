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
    public class FeedbackServiceTests : InMemoryDbTest
    {
        public FeedbackService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new FeedbackService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IFeedbackService
//     {
//         Task<int> CreateFeedbackAsync(Feedback feedback);
//     }
