using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Prime;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Utils.Auth
{
    public class TestAuthenticationContext
    {
        public TestServer Server { get; set; }

        public IConfigurationRoot ConfigSettings { get; set; }

        public HttpClient Client { get; set; }

        public ApiDbContext ApiDbContext { get; set; }

        public BearerTokenBuilder TokenBuilder { get; set; }

        private const string CertificatePassword = "prime-api";

        // public TestAuthenticationContext() : this(Server.Host.Services.GetService(typeof(ApiDbContext)) as ApiDbContext)
        // {
        //     // TestAuthenticationContext(Server.Host.Services.GetService(typeof(ApiDbContext)) as ApiDbContext);
        // }

        public TestAuthenticationContext(string _databaseName)
        {
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>()
                .ConfigureServices(services =>
                {
                    services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            SignatureValidator = (token, parameters) => new JwtSecurityToken(token),
                            RoleClaimType = System.Security.Claims.ClaimTypes.Role
                        };
                        //options.Audience = TestAuthorizationConstants.Audience;
                        options.Authority = TestAuthorizationConstants.Issuer;
                        options.BackchannelHttpHandler = new MockBackchannel();
                        options.MetadataAddress = "https://inmemory.microsoft.com/common/.well-known/openid-configuration";
                    });
                    var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ApiDbContext));
                    services.Remove(serviceDescriptor);
                    services.AddDbContext<ApiDbContext>(options =>
                        options.UseInMemoryDatabase(databaseName: _databaseName)
                    );
                });

            Server = new TestServer(builder);

            Client = Server.CreateClient();

            TokenBuilder =
                    new BearerTokenBuilder()
                        .ForAudience(TestAuthorizationConstants.Audience)
                        .IssuedBy(TestAuthorizationConstants.Issuer)
                        .WithSigningCertificate(EmbeddedResourceReader.GetCertificate(CertificatePassword));

            ApiDbContext = Server.Host.Services.GetService(typeof(ApiDbContext)) as ApiDbContext;
        }

        public Enrolment CreateEnrolment()
        {
            IEnrolmentService service = Server.Host.Services.GetService(typeof(IEnrolmentService)) as IEnrolmentService;
            var enrolment = TestUtils.EnrolmentFaker.Generate();
            int enrolmentId = (int)service.CreateEnrolmentAsync(enrolment).Result;

            return service.GetEnrolmentAsync(enrolmentId).Result;
        }

        public Enrolment GetEnrolmentById(int enrolmentId)
        {
            IEnrolmentService service = Server.Host.Services.GetService(typeof(IEnrolmentService)) as IEnrolmentService;

            return service.GetEnrolmentAsync(enrolmentId).Result;
        }

    }
}