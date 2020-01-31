using Xunit.Abstractions;
using PrimeTests.Utils;

namespace PrimeTests.Controllers
{
    public class EnrolleesAccessTermsControllerTests : BaseControllerTests
    {
        public EnrolleesAccessTermsControllerTests(CustomWebApplicationFactory<TestStartup> factory, ITestOutputHelper output) : base(factory)
        {
        }
    }
}
