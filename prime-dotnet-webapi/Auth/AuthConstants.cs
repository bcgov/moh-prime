using System;

namespace Prime.Auth
{
    public static class AuthConstants
    {
        public const string API_AUDIENCE = "prime-web-api";
        public const string KEYCLOAK_REALM_ACCESS_KEY = "realm_access";
        public const string BC_SERVICES_CARD = "bcsc";
    }

    public static class Claims
    {
        public const string AuthorizedParty = "azp";
        public const string AssuranceLevel = "identity_assurance_level";
        public const string IdentityProvider = "identity_provider";
    }

    public static class Roles
    {
        public const string User = "prime_user";
        public const string ReadonlyAdmin = "prime_readonly_admin";
        public const string Admin = "prime_admin";
        public const string SuperAdmin = "prime_super_admin";
    }

    public static class Policies
    {
        public const string EnrolleeOnly = "enrollee-policy";
        public const string AdminOnly = "admin-policy";
        public const string SiteRegistrantOnly = "site-policy";
        public const string EnrolleeOrAdmin = "enrollee-admin-policy";
        public const string SiteRegistrantOrAdmin = "enrollee-admin-policy";
        public const string AnyUser = "any-user-policy";

        public const string CanEdit = "can-edit-policy";

        public const string CareConnectAccess = "hpdid-access-policy";
        public const string PosGpidAccess = "pos-gpid-access-policy";
    }

    public static class AuthorizedParties
    {
        public const string Admin = "prime-application-admin";
        public const string Enrolment = "prime-application-enrolment";
        public const string Site = "prime-application-site";
        public const string CareConnect = "prime-careconnect-access";
        public const string PosGpid = "prime-pos-gpid";
    }

    public static class FeatureFlags
    {
        public const string SiteRegistration = "feature_site_registration";
        public const string CredentialIssuance = "feature_vc_issuance";
    }
}
