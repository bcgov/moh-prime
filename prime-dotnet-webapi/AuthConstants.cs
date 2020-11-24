namespace Prime.Auth
{
    public static class AuthConstants
    {
        public const string Audience = "prime-web-api";

        public const string BCServicesCard = "bcsc";
    }

    public static class Claims
    {
        public const string RealmAccess = "realm_access";
        public const string AssuranceLevel = "identity_assurance_level";
        public const string IdentityProvider = "identity_provider";
        public const string Address = "address";
    }

    public static class FeatureFlags
    {
        public const string SiteRegistration = "feature_site_registration";
        public const string VCIssuance = "feature_vc_issuance";
    }

    public static class Roles
    {
        public const string PrimeSuperAdmin = "prime_super_admin";
        public const string PrimeAdmin = "prime_admin";
        public const string PrimeReadonlyAdmin = "prime_readonly_admin";
        public const string PrimeEnrollee = "prime_user";
        public const string ExternalHpdidAccess = "external_hpdid_access";
        public const string ExternalGpidValidation = "external_gpid_validation";
        public const string PrimeApiServiceAccount = "prime_api_service_account";
    }

    public static class Policies
    {
        public const string SuperAdmin = "super-admin-policy";
        public const string Admin = "admin-policy";
        public const string ReadonlyAdmin = "readonly-admin-policy";
        public const string User = "user-policy";
        public const string ExternalHpdidAccess = "external-hpdid-access-policy";
        public const string ExternalGpidValidation = "external-gpid-validation-policy";
    }
}
