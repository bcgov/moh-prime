using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using Prime.Services;
using PrimeTests.Mocks;
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
                        //add the mock service mapping, so that we are only testing the controllers
                        services.AddSingleton<IEnrolmentService, EnrolmentServiceMock>();
                        services.AddSingleton<IEnrolleeService, EnrolleeServiceMock>();
                        services.AddSingleton<ILookupService, LookupServiceMock>();
                    });
                });
            _client = _factory.CreateClient();
        }
    }
}