namespace Prime
{
    public static class PrimeConstants
    {
        public const string PRIME_ACCESS_TOKEN_KEY = "prime-access-token";
        
        public const string PRIME_ADMIN_CLIENT = "prime-admin";

        public const string PRIME_USER_CLIENT = "prime-user";

        public readonly static string[] PRIME_CLIENT_IDS = {PRIME_ADMIN_CLIENT, PRIME_USER_CLIENT};

        public const string PRIME_ADMIN_ROLE = "prime-admin-client";

        public const string PRIME_ADMIN_POLICY = "prime-admin-policy";

        public const string PRIME_ENROLMENT_ROLE = "prime-user-client";
        
        public const string PRIME_USER_POLICY = "prime-user-policy";

    }
}