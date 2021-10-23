using Pip.Configuration.Internal;

namespace Pip
{
    public class PipConfiguration
    {
        public static PipConfiguration? Current { get; set; }

        public static readonly string LogFilePath = Environment.GetEnvironmentVariable("LogFilePath") ?? "logs";
        public static bool IsDevelopment() => EnvironmentName == Environments.Development;
        private static readonly string? EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public string FrontendUrl { get; set; }
        public string BackendUrl { get; set; }

        public ConnectionStringConfiguration ConnectionStrings { get; set; } = new ConnectionStringConfiguration();
    }
}
