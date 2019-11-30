using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Prime;
using Prime.Models;

namespace PrimeTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env, IConfiguration configuration) 
            : base(env, TestHelper.GetIConfigurationRoot(Directory.GetCurrentDirectory()))
        {}

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "PrimeTests")
            );
            
            object p = services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Prime")));
        }

        public override void UpdateDatabase(IApplicationBuilder app)
        {
            //noop, since the tests are using the InMemoryDatabase
        }
    }

}