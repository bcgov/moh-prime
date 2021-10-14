using Microsoft.Extensions.Hosting;
using System;

using Pip.Configuration.Internal;

namespace Pip
{
    public class PipConfiguration
    {
        public static PipConfiguration? Current { get; set; }

        public static readonly string LogFilePath = Environment.GetEnvironmentVariable("LOG_FILE_PATH") ?? "logs";
        public static bool IsDevelopment() => EnvironmentName == Environments.Development;
        public static bool IsProduction() => EnvironmentName == Environments.Production;
        private static readonly string? EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public ConnectionStringConfiguration ConnectionStrings { get; set; } = new ConnectionStringConfiguration();
    }
}
