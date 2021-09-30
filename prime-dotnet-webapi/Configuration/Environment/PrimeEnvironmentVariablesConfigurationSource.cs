using Microsoft.Extensions.Configuration;

namespace Prime.Configuration.Environment
{
    public class PrimeEnvironmentVariablesConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PrimeEnvironmentVariablesConfigurationProvider();
        }
    }
}

