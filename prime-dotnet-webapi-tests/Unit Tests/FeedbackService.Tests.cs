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
    public class FeedbackServiceTests
    {
        public static FeedbackService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null)
        {
            return new FeedbackService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IFeedbackService
//     {
//         Task<int> CreateFeedbackAsync(Feedback feedback);
//     }
