using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Prime;
using PrimeTests.Utils;
using Xunit;

namespace PrimeTests.Integration
{
    public class BaseIntegrationTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>, IDisposable
    {
        protected readonly WebApplicationFactory<TestStartup> _factory;
        protected readonly HttpClient _client;

        public BaseIntegrationTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                TestUtils.ReinitializeDbForTests(_dbContext);
            }
        }

        public void Dispose()
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                TestUtils.DetachAllEntities(_dbContext);
                _dbContext.Database.EnsureDeleted();
                _dbContext.Dispose();
            }
        }
    }
}