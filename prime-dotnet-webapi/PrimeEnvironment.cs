using static System.Environment;

using Prime.Auth;

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

        public static class PrimeKeycloak
        {
            public static readonly string RealmUrl = GetEnvironmentVariable("KEYCLOAK_REALM_URL") ?? "https://dev.oidc.gov.bc.ca/auth/realms/v4mbqqas";
            public static readonly string WellKnownConfig = KeycloakUrls.WellKnownConfig(RealmUrl);
            public static readonly string TokenUrl = KeycloakUrls.Token(RealmUrl);
            public static readonly string AdministrationUrl = GetEnvironmentVariable("KEYCLOAK_ADMINISTRATION_URL") ?? "https://dev.oidc.gov.bc.ca/auth/admin/realms/v4mbqqas";
            public static readonly string AdministrationClientId = "keycloak-service-account";
            public static readonly string AdministrationClientSecret = GetEnvironmentVariable("KEYCLOAK_ADMINISTRATION_CLIENT_SECRET") ?? "";
        }

        public static class MohKeycloak
        {
            public static readonly string RealmUrl = GetEnvironmentVariable("MOH_KEYCLOAK_REALM_URL") ?? "https://common-logon-dev.hlth.gov.bc.ca/auth/realms/moh_applications";
            public static readonly string WellKnownConfig = KeycloakUrls.WellKnownConfig(RealmUrl);
            public static readonly string TokenUrl = KeycloakUrls.Token(RealmUrl);
            public static readonly string GisClientId = "GIS";
            public static readonly string AdministrationUrl = GetEnvironmentVariable("MOH_KEYCLOAK_ADMINISTRATION_URL") ?? "https://user-management-dev.api.hlth.gov.bc.ca";
            public static readonly string AdministrationClientId = "PRIME-WEBAPP-ENROLLMENT";
            public static readonly string AdministrationClientSecret = GetEnvironmentVariable("MOH_KEYCLOAK_ADMINISTRATION_CLIENT_SECRET") ?? "";
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
            public static readonly string Url = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_URL") ?? "http://agent:8024/";
            public static readonly string Key = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_KEY") ?? "agent-api-key-dev";
            public static readonly string WebhookKey = GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_WEBHOOK_KEY") ?? "0ce755d5-1fb1-483a-ba22-439061aa8f67";
            // If schema changes, the following must be updated in all agents for each environment as the code changes are pushed so versions are the same
            // and have verifier app updated by aries team in each environment (send them schema id, if claims change send them new attributes)
            // Update the following through postman:
            // 1. Add new schema, incrementing schema version -> schema_name = enrollee
            // 2. Create a credential definition for schema -> support_revocation = true, tag = prime
            public static readonly string SchemaName = "enrollee";
            public static readonly string SchemaVersion = "2.2";
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

        public static class PlrIntegration
        {
            public static readonly string ClientCertThumbprint = GetEnvironmentVariable("PLR_INTEGRATION_CLIENT_CERT_THUMBPRINT");
        }

        public static class LdapApi
        {
            public static readonly string Url = GetEnvironmentVariable("LDAP_API_URL") ?? "https://common-logon-dev.hlth.gov.bc.ca/ldap";
        }
    }
}
