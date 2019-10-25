namespace Prime
{
    public static class PrimeConstants
    {
        public const string PRIME_ACCESS_TOKEN_KEY = "prime-access-token";

        public readonly static string PRIME_ADMIN_CLIENT = Startup.StaticConfig["Jwt:AdminClient"];

        public readonly static string PRIME_USER_CLIENT = Startup.StaticConfig["Jwt:UserClient"];

        public readonly static string[] PRIME_CLIENT_IDS = { PRIME_ADMIN_CLIENT, PRIME_USER_CLIENT };

        public const string PRIME_ADMIN_ROLE = "prime_admin";

        public const string PRIME_ADMIN_POLICY = "prime-admin-policy";

        public const string PRIME_ENROLMENT_ROLE = "prime_user";

        public const string PRIME_USER_POLICY = "prime-user-policy";

        public const string ASSURANCE_LEVEL_CLAIM_TYPE = "identity_assurance_level";

        // add some constants for the KEYCLOAK access token keys
        public const string KEYCLOAK_REALM_ACCESS_KEY = "realm_access";

        public const string KEYCLOAK_RESOURCE_ACCESS_KEY = "resource_access";
        
        public const string KEYCLOAK_ROLES_KEY = "roles";

    }
}