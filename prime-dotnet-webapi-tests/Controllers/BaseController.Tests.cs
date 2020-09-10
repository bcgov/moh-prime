using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

using PrimeTests.Utils;

namespace PrimeTests.Controllers
{
    public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        protected readonly WebApplicationFactory<TestStartup> _factory;
        protected readonly HttpClient _client;

        public BaseControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {

                    });
                });
            _client = _factory.CreateClient();
        }
    }
}
