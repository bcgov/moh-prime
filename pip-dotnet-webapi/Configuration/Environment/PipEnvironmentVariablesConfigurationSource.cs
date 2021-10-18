namespace Pip.Configuration.Environment
{
    public class PipEnvironmentVariablesConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new PipEnvironmentVariablesConfigurationProvider();
        }
    }
}

