namespace Prime
{
    public static class PrimeConstants
    {
        public readonly static string ENVIRONMENT_NAME = System.Environment.GetEnvironmentVariable("OC_APP") ?? "local";

        public readonly static string PRIME_ACCESS_TOKEN_KEY = "prime-access-token";

        public readonly static string PRIME_ADMIN_CLIENT = System.Environment.GetEnvironmentVariable("JWT_ADMIN_CLIENT") ?? Startup.StaticConfig["Jwt:AdminClient"];

        public readonly static string PRIME_USER_CLIENT = System.Environment.GetEnvironmentVariable("JWT_USER_CLIENT") ?? Startup.StaticConfig["Jwt:UserClient"];

        public readonly static string[] PRIME_CLIENT_IDS = { PRIME_ADMIN_CLIENT, PRIME_USER_CLIENT };

        public readonly static string PRIME_ADMIN_ROLE = "prime_admin";

        public readonly static string PRIME_ENROLLEE_ROLE = "prime_user";

        public readonly static string ASSURANCE_LEVEL_CLAIM_TYPE = "identity_assurance_level";

        // add some constants for the KEYCLOAK access token keys
        public readonly static string KEYCLOAK_REALM_ACCESS_KEY = "realm_access";

        public readonly static string KEYCLOAK_RESOURCE_ACCESS_KEY = "resource_access";

        public readonly static string KEYCLOAK_ROLES_KEY = "roles";

        // add some constants for the auth policies - note: these need to be consts so they can be used in annotations
        public const string PRIME_ADMIN_POLICY = "prime-admin-policy";

        public const string PRIME_USER_POLICY = "prime-user-policy";

        // Enrollee Classifications
        public readonly static string PRIME_MOA = "MOA";

        public readonly static string PRIME_RU = "RU";

        // Mail server
        public readonly static string MAIL_SERVER_URL = System.Environment.GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
        public readonly static int MAIL_SERVER_PORT = int.Parse(System.Environment.GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");
    }
}
