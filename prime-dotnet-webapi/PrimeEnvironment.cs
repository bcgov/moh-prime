using static System.Environment;

namespace Prime
{
    public static class PrimeEnvironment
    {
        public static readonly string Name = GetEnvironmentVariable("OC_APP") ?? "local";
        public static readonly string FrontendUrl = GetEnvironmentVariable("FRONTEND_URL") ?? "localhost:4200";
        public static readonly string BackendUrl = GetEnvironmentVariable("BACKEND_URL") ?? "http://localhost:5000/api";
        public static readonly string LogFile = GetEnvironmentVariable("LOG_FILE_PATH") ?? "logs";

        public static bool IsProduction { get => Name == "prod"; }
        public static bool IsLocal { get => Name == "local"; }

        public static class DocumentManager
        {
            public static readonly string Url = GetEnvironmentVariable("DOCUMENT_MANAGER_URL") ?? "http://localhost:6001";
            public static readonly string ClientId = GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_ID") ?? "prime-document-manager-local";
            public static readonly string ClientSecret = GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_SECRET") ?? "b515de16-419b-49b1-bca9-f97eafc95d41";
        }

        public static class Keycloak
        {
            public static readonly string WellKnownConfig = GetEnvironmentVariable("JWT_WELL_KNOWN_CONFIG") ?? "https://dev.oidc.gov.bc.ca/auth/realms/v4mbqqas/.well-known/openid-configuration";
            public static readonly string TokenUrl = GetEnvironmentVariable("KEYCLOAK_TOKEN_URL") ?? "https://dev.oidc.gov.bc.ca/auth/realms/v4mbqqas/protocol/openid-connect/token";
            public static readonly string AdministrationUrl = GetEnvironmentVariable("KEYCLOAK_ADMINISTRATION_URL") ?? "https://dev.oidc.gov.bc.ca/auth/admin/realms/v4mbqqas";
            public static readonly string AdministrationClientId = "keycloak-service-account";
            public static readonly string AdministrationClientSecret = GetEnvironmentVariable("KEYCLOAK_ADMINISTRATION_CLIENT_SECRET") ?? "";
        }

        public static class MailServer
        {
            public static readonly string Url = GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
            public static readonly int Port = int.Parse(GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");
        }

        public static class PharmanetApi
        {
            public static readonly string Url = GetEnvironmentVariable("PHARMANET_API_URL");
            public static readonly string Username = GetEnvironmentVariable("PHARMANET_API_USERNAME");
            public static readonly string Password = GetEnvironmentVariable("PHARMANET_API_PASSWORD");
            public static readonly string SslCertFilename = GetEnvironmentVariable("PHARMANET_SSL_CERT_FILENAME");
            public static readonly string SslCertPassword = GetEnvironmentVariable("PHARMANET_SSL_CERT_PASSWORD");
        }

        public static class ChesApi
        {
            public static readonly bool Enabled = bool.Parse(GetEnvironmentVariable("CHES_ENABLED") ?? "false");
            public static readonly string Url = GetEnvironmentVariable("CHES_API_URL") ?? "https://ches-dev.pathfinder.gov.bc.ca/api/v1";
            public static readonly string ClientId = "PRIME_SERVICE_CLIENT";
            public static readonly string ClientSecret = GetEnvironmentVariable("CHES_CLIENT_SECRET") ?? "88e123a6-80cb-46a0-96d3-e2edae076ae7";
            public static readonly string TokenUrl = GetEnvironmentVariable("CHES_TOKEN_URL") ?? "https://dev.oidc.gov.bc.ca/auth/realms/jbd6rnxw/protocol/openid-connect";
        }

        /// <summary>
        /// Aries Prime Agent
        /// </summary>
        public static class VerifiableCredentialApi
        {
            public static readonly string Url = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_URL") ?? "https://prime-agent-admin-dev.pathfinder.gov.bc.ca/";
            public static readonly string Key = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_KEY") ?? "P8ZmRJ05biXGWI1/bDtXcp1pixtWdsAqhcUJcn4S7QQ=";
            public static readonly string WebhookKey = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_WEBHOOK_KEY") ?? "0ce755d5-1fb1-483a-ba22-439061aa8f67";
        }

        /// <summary>
        /// Canada Post Address Validation
        /// </summary>
        public static class AddressAutocompleteApi
        {
            public static readonly string Url = GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_URL") ?? "https://ws1.postescanada-canadapost.ca/AddressComplete/Interactive/";
            public static readonly string Key = GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_KEY") ?? "XC95-NM62-BR64-TG37";
        }

        public static class MetabaseApi
        {
            public static readonly string Url = GetEnvironmentVariable("METABASE_SITE_URL") ?? "https://metabase-test.pharmanetenrolment.gov.bc.ca";
            public static readonly string Key = GetEnvironmentVariable("METABASE_SECRET_KEY") ?? "f7a2ffbc8ebd7e273603896c0f63ae04a596b94ff76c276398ce5aa8ca216cee";
            public static readonly int DashboardId = int.Parse(GetEnvironmentVariable("METABASE_DASHBOARD_ID") ?? "4");
        }

        public static class LdapApi
        {
            public static readonly string Url = GetEnvironmentVariable("LDAP_API_URL") ?? "https://common-logon-dev.hlth.gov.bc.ca/ldap";
        }
    }
}
