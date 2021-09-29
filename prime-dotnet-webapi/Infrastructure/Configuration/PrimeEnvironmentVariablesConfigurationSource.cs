using Microsoft.Extensions.Configuration;

namespace Prime.Infrastructure.Configuration
{
    public class PrimeEnvironmentVariablesConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PrimeEnvironmentVariablesConfigurationProvider();
        }
    }
}

