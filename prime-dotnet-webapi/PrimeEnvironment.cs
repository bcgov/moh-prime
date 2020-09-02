using System;

namespace Prime
{
    public static class PrimeEnvironment
    {
        public readonly static string Name = Environment.GetEnvironmentVariable("OC_APP") ?? "local";
        public readonly static string FrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "localhost:4200";
        public readonly static string BackendUrl = Environment.GetEnvironmentVariable("BACKEND_URL") ?? "http://localhost:5000/api";
        public readonly static string KeycloakTokenUrl = Environment.GetEnvironmentVariable("KEYCLOAK_TOKEN_URL") ?? "https://sso-dev.pathfinder.gov.bc.ca/auth/realms/v4mbqqas/protocol/openid-connect/token";
        public readonly static string LogFile = Environment.GetEnvironmentVariable("LOG_FILE_PATH") ?? "logs";

        public static bool IsProduction { get => Name == "prod"; }
        public static bool IsLocal { get => Name == "local"; }

        public static class MailServer
        {
            public readonly static string Url = Environment.GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
            public readonly static int Port = int.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");
        }

        public static class PharmanetApi
        {
            public readonly static string Url = Environment.GetEnvironmentVariable("PHARMANET_API_URL");
            public readonly static string Username = Environment.GetEnvironmentVariable("PHARMANET_API_USERNAME");
            public readonly static string Password = Environment.GetEnvironmentVariable("PHARMANET_API_PASSWORD");
            public readonly static string SslCertFilename = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_FILENAME");
            public readonly static string SslCertPassword = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_PASSWORD");
        }

        public static class ChesApi
        {
            public readonly static bool Enabled = bool.Parse(Environment.GetEnvironmentVariable("CHES_ENABLED") ?? "false");
            public readonly static string Url = Environment.GetEnvironmentVariable("CHES_API_URL") ?? "https://ches-master-9f0fbe-dev.pathfinder.gov.bc.ca/api/v1";
            public readonly static string ClientId = "PRIME_SERVICE_CLIENT";
            public readonly static string ClientSecret = Environment.GetEnvironmentVariable("CHES_CLIENT_SECRET") ?? "88e123a6-80cb-46a0-96d3-e2edae076ae7";
            public readonly static string TokenUrl = Environment.GetEnvironmentVariable("CHES_TOKEN_URL") ?? "https://sso-dev.pathfinder.gov.bc.ca/auth/realms/jbd6rnxw/protocol/openid-connect";
        }

        public static class DocumentManager
        {
            public readonly static string Url = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_URL") ?? "http://localhost:6001/";
            public readonly static string ClientId = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_ID") ?? "prime-document-manager-local";
            public readonly static string ClientSecret = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_SECRET") ?? "b515de16-419b-49b1-bca9-f97eafc95d41";
        }

        // Aries Prime Agent
        public static class VerifiableCredentialApi
        {
            public readonly static string Url = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_URL") ?? "https://prime-agent-admin-dev.pathfinder.gov.bc.ca/";
            public readonly static string Key = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_KEY") ?? "P8ZmRJ05biXGWI1/bDtXcp1pixtWdsAqhcUJcn4S7QQ=";
            public readonly static string WebhookKey = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_WEBHOOK_KEY") ?? "0ce755d5-1fb1-483a-ba22-439061aa8f67";
        }

        // Canada Post Address Validation
        public static class AddressAutocompleteApi
        {
            public readonly static string Url = Environment.GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_URL") ?? "https://ws1.postescanada-canadapost.ca/AddressComplete/Interactive/";
            public readonly static string Key = Environment.GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_KEY") ?? "XC95-NM62-BR64-TG37";
        }
    }
}
