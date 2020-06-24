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

        // Claims
        public const string AUDIENCE_CLAIM = "aud";
        public const string ASSURANCE_LEVEL_CLAIM = "identity_assurance_level";
        public const string IDENTITY_PROVIDER_CLAIM = "identity_provider";
        public const string BC_SERVICES_CARD = "bcsc";

        // Roles
        public const string PRIME_SUPER_ADMIN_ROLE = "prime_super_admin";
        public const string PRIME_ADMIN_ROLE = "prime_admin";
        public const string PRIME_USER_ROLE = "prime_user";
        public const string PRIME_READONLY_ADMIN = "prime_readonly_admin";
        public const string EXTERNAL_HPDID_ACCESS_ROLE = "external_hpdid_access";

        // Feature Flags
        public const string FEATURE_SITE_REGISTRATION = "feature_site_registration";
    }

    public static class Policies
    {
        public const string Enrollee = "enrollee-policy";
        public const string AdminView = "admin-view-policy";
        public const string Admin = "admin-policy";
        public const string SuperAdmin = "super-admin-policy";
        public const string HpdidAccess = "hpdid-access-policy";
        public const string PosGpidAccess = "pos-gpid-access-policy";
    }

    public static class Audiences
    {
        public const string Admin = "prime-application-admin";
        public const string Enrolment = "prime-application-enrolment";
        public const string Site = "prime-application-site";
        public const string CareConnect = "prime-careconnect-access";
        public const string GpidAccess = "prime-pos-gpid";
    }
}
