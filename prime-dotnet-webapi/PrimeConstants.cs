using System;

namespace Prime
{
    public static class PrimeConstants
    {
        public readonly static string ENVIRONMENT_NAME = Environment.GetEnvironmentVariable("OC_APP") ?? "local";

        public readonly static string FRONTEND_URL = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "localhost:4200";

        public const string PRIME_ACCESS_TOKEN_KEY = "prime-access-token";

        public readonly static string PRIME_ADMIN_CLIENT = Environment.GetEnvironmentVariable("JWT_ADMIN_CLIENT") ?? Startup.StaticConfig["Jwt:AdminClient"];

        public readonly static string PRIME_USER_CLIENT = Environment.GetEnvironmentVariable("JWT_USER_CLIENT") ?? Startup.StaticConfig["Jwt:UserClient"];

        public readonly static string[] PRIME_CLIENT_IDS = { PRIME_ADMIN_CLIENT, PRIME_USER_CLIENT };

        // Auth
        public const string PRIME_SUPER_ADMIN_ROLE = "prime_super_admin";
        public const string PRIME_ADMIN_ROLE = "prime_admin";
        public const string PRIME_ENROLLEE_ROLE = "prime_user";
        public const string PRIME_READONLY_ADMIN = "prime_readonly_admin";
        public const string EXTERNAL_HPDID_ACCESS_ROLE = "external_hpdid_access";
        public const string ASSURANCE_LEVEL_CLAIM_TYPE = "identity_assurance_level";
        public const string KEYCLOAK_REALM_ACCESS_KEY = "realm_access";
        public const string KEYCLOAK_RESOURCE_ACCESS_KEY = "resource_access";
        public const string KEYCLOAK_ROLES_KEY = "roles";
        public const string SUPER_ADMIN_POLICY = "super-admin-policy";
        public const string READONLY_ADMIN_POLICY = "readonly-admin-policy";
        public const string ADMIN_POLICY = "admin-policy";
        public const string USER_POLICY = "user-policy";
        public const string EXTERNAL_HPDID_ACCESS_POLICY = "external-hpdid-access-policy";


        // Enrollee Classifications
        public const string PRIME_OBO = "OBO";
        public const string PRIME_RU = "RU";

        // Mail server
        public readonly static string MAIL_SERVER_URL = Environment.GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
        public readonly static int MAIL_SERVER_PORT = int.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");

        // Pharmanet college validation API
        public readonly static string PHARMANET_API_URL = Environment.GetEnvironmentVariable("PHARMANET_API_URL") ?? "";
        public readonly static string PHARMANET_API_USERNAME = Environment.GetEnvironmentVariable("PHARMANET_API_USERNAME") ?? "";
        public readonly static string PHARMANET_API_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_API_PASSWORD") ?? "";
        public readonly static string PHARMANET_SSL_CERT_FILENAME = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_FILENAME") ?? "";
        public readonly static string PHARMANET_SSL_CERT_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_PASSWORD") ?? "";
    }
}
