using Microsoft.Extensions.Hosting;
using Serilog.Events;
using System;

using Prime.Configuration.Internal;

namespace Prime
{
    public class PrimeConfiguration
    {
        public static PrimeConfiguration Current { get; set; }

        public static readonly string LogFilePath = Environment.GetEnvironmentVariable("LOG_FILE_PATH") ?? "logs";
        public static readonly LogEventLevel LogLevel = ParseLogLevel(); // Default is Information but can be optionally overridden by the environment variable "LogLevel"
        public static bool IsDevelopment() => EnvironmentName == Environments.Development;
        public static bool IsProduction() => EnvironmentName == Environments.Production;
        private static readonly string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public string FrontendUrl { get; set; }
        public string BackendUrl { get; set; }

        public ConnectionStringConfiguration ConnectionStrings { get; set; }

        public DocumentManagerConfiguration DocumentManager { get; set; }

        public PrimeKeycloakConfiguration PrimeKeycloak { get; set; }

        public MohKeycloakConfiguration MohKeycloak { get; set; }

        public MailServerConfiguration MailServer { get; set; }

        public PharmanetApiConfiguration PharmanetApi { get; set; }

        public ChesApiConfiguration ChesApi { get; set; }

        /// <summary>
        /// Aries Prime Agent
        /// </summary>
        public VerifiableCredentialApiConfiguration VerifiableCredentialApi { get; set; }

        /// <summary>
        /// Canada Post Address Validation
        /// </summary>
        public AddressAutocompleteApiConfiguration AddressAutocompleteApi { get; set; }

        public MetabaseApiConfiguration MetabaseApi { get; set; }

        public PlrIntegrationConfiguration PlrIntegration { get; set; }

        public LdapApiConfiguration LdapApi { get; set; }

        private static LogEventLevel ParseLogLevel()
        {
            if (int.TryParse(Environment.GetEnvironmentVariable("LogLevel"), out var logLevel))
            {
                return (LogEventLevel)logLevel;
            }

            return LogEventLevel.Information;
        }
    }
}
