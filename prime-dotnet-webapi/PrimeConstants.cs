using System;

namespace Prime
{
    public static class PrimeConstants
    {
        public readonly static string ENVIRONMENT_NAME = Environment.GetEnvironmentVariable("OC_APP") ?? "local";
        public readonly static string FRONTEND_URL = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "localhost:4200";
        public readonly static string BACKEND_URL = Environment.GetEnvironmentVariable("BACKEND_URL") ?? "http://localhost:5000/api";
        public readonly static string KEYCLOAK_TOKEN_URL = Environment.GetEnvironmentVariable("KEYCLOAK_TOKEN_URL") ?? "https://sso-dev.pathfinder.gov.bc.ca/auth/realms/v4mbqqas/protocol/openid-connect/token";

        // Enrollee Classifications
        public const string PRIME_OBO = "OBO";
        public const string PRIME_RU = "RU";

        // Mail server
        public readonly static string MAIL_SERVER_URL = Environment.GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
        public readonly static int MAIL_SERVER_PORT = int.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");

        // Pharmanet college validation API
        public readonly static string PHARMANET_API_URL = Environment.GetEnvironmentVariable("PHARMANET_API_URL");
        public readonly static string PHARMANET_API_USERNAME = Environment.GetEnvironmentVariable("PHARMANET_API_USERNAME");
        public readonly static string PHARMANET_API_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_API_PASSWORD");
        public readonly static string PHARMANET_SSL_CERT_FILENAME = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_FILENAME");
        public readonly static string PHARMANET_SSL_CERT_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_PASSWORD");

        // CHES Email API
        public readonly static string CHES_ENABLED = Environment.GetEnvironmentVariable("CHES_ENABLED") ?? "false";
        public readonly static string CHES_API_URL = Environment.GetEnvironmentVariable("CHES_API_URL") ?? "https://ches-master-9f0fbe-dev.pathfinder.gov.bc.ca/api/v1";
        public readonly static string CHES_CLIENT_ID = "PRIME_SERVICE_CLIENT";
        public readonly static string CHES_CLIENT_SECRET = Environment.GetEnvironmentVariable("CHES_CLIENT_SECRET") ?? "88e123a6-80cb-46a0-96d3-e2edae076ae7";
        public readonly static string CHES_TOKEN_URL = Environment.GetEnvironmentVariable("CHES_TOKEN_URL") ?? "https://sso-dev.pathfinder.gov.bc.ca/auth/realms/jbd6rnxw/protocol/openid-connect";

        // Logging
        public readonly static string LOG_FILE_PATH = Environment.GetEnvironmentVariable("LOG_FILE_PATH") ?? "logs";

        // Document Manager
        public readonly static string DOCUMENT_MANAGER_URL = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_URL") ?? "http://localhost:6001/";
        public readonly static string DOCUMENT_MANAGER_CLIENT_ID = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_ID") ?? "prime-document-manager-local";
        public readonly static string DOCUMENT_MANAGER_CLIENT_SECRET = Environment.GetEnvironmentVariable("DOCUMENT_MANAGER_CLIENT_SECRET") ?? "b515de16-419b-49b1-bca9-f97eafc95d41";

        // Aries Prime Agent
        public readonly static string VERIFIABLE_CREDENTIAL_API_URL = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_URL") ?? "https://prime-agent-admin-dev.pathfinder.gov.bc.ca/";
        public readonly static string VERIFIABLE_CREDENTIAL_API_KEY = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_API_KEY") ?? "P8ZmRJ05biXGWI1/bDtXcp1pixtWdsAqhcUJcn4S7QQ=";
        public readonly static string VERIFIABLE_CREDENTIAL_WEBHOOK_KEY = Environment.GetEnvironmentVariable("VERIFIABLE_CREDENTIAL_WEBHOOK_KEY") ?? "0ce755d5-1fb1-483a-ba22-439061aa8f67";

        // Canada Post Address Validation
        public readonly static string ADDRESS_AUTOCOMPLETE_API_URL = Environment.GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_URL") ?? "https://ws1.postescanada-canadapost.ca/AddressComplete/Interactive/";
        public readonly static string ADDRESS_AUTOCOMPLETE_API_KEY = Environment.GetEnvironmentVariable("ADDRESS_AUTOCOMPLETE_API_KEY") ?? "XC95-NM62-BR64-TG37";

    }
}
