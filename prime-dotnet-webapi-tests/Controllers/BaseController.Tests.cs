using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Prime.Services;
using PrimeTests.Mocks;
using PrimeTests.Utils;
using Xunit;

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
                        services.AddSingleton<IEnrolmentService, EnrolmentServiceMock>();
                        services.AddSingleton<ILookupService, LookupServiceMock>();
                    });
                });
            _client = _factory.CreateClient();
        }
        
    }
}