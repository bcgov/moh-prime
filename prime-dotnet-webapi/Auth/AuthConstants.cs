using System;

namespace Prime.Auth
{
    public static class AuthConstants
    {
        public const string PRIME_ACCESS_TOKEN_KEY = "prime-access-token";
        public const string KEYCLOAK_ROLES_KEY = "roles";
        public const string KEYCLOAK_REALM_ACCESS_KEY = "realm_access";
        public const string KEYCLOAK_RESOURCE_ACCESS_KEY = "resource_access";
        public readonly static string PRIME_ADMIN_CLIENT = Environment.GetEnvironmentVariable("JWT_ADMIN_CLIENT") ?? Startup.StaticConfig["Jwt:AdminClient"];
        public readonly static string PRIME_USER_CLIENT = Environment.GetEnvironmentVariable("JWT_USER_CLIENT") ?? Startup.StaticConfig["Jwt:UserClient"];
        public readonly static string[] PRIME_CLIENT_IDS = { PRIME_ADMIN_CLIENT, PRIME_USER_CLIENT };
        public const string ADMIN_AUDIENCE = "prime-application-admin";
        public const string ENROLMENT_AUDIENCE = "prime-application-enrolment";
        public const string SITE_AUDIENCE = "prime-application-site";
        public const string CARECONNECT_AUDIENCE = "prime-careconnect-access";
        public const string POS_GPID_AUDIENCE = "prime-pos-gpid";

        // Claims
        public const string ASSURANCE_LEVEL_CLAIM_TYPE = "identity_assurance_level";
        public const string IDENTITY_PROVIDER_CLAIM_TYPE = "identity_provider";
        public const string BC_SERVICES_CARD = "bcsc";

        // Roles
        public const string PRIME_SUPER_ADMIN_ROLE = "prime_super_admin";
        public const string PRIME_ADMIN_ROLE = "prime_admin";
        public const string PRIME_ENROLLEE_ROLE = "prime_user";
        public const string PRIME_READONLY_ADMIN = "prime_readonly_admin";
        public const string EXTERNAL_HPDID_ACCESS_ROLE = "external_hpdid_access";
        public const string EXTERNAL_GPID_VALIDATION_ROLE = "external_gpid_validation";

        // Feature Flags
        public const string FEATURE_SITE_REGISTRATION = "feature_site_registration";
        public const string FEATURE_VC_ISSUANCE = "feature_vc_issuance";

        // Policies
        public const string SUPER_ADMIN_POLICY = "super-admin-policy";
        public const string READONLY_ADMIN_POLICY = "readonly-admin-policy";
        public const string ADMIN_POLICY = "admin-policy";
        public const string USER_POLICY = "user-policy";
        public const string EXTERNAL_HPDID_ACCESS_POLICY = "external-hpdid-access-policy";
        public const string EXTERNAL_GPID_VALIDATION_POLICY = "external-gpid-validation-policy";
    }
}
