namespace Prime.Auth
{
    public static class AuthConstants
    {
        public const string Audience = "prime-web-api";
        public const string BCServicesCard = "bcsc";
    }

    public static class Schemes
    {
        public const string PrimeJwt = "PrimeJwt";
        public const string MohJwt = "MohJwt";
    }

    public static class Claims
    {
        public const string PreferredUsername = "preferred_username";
        public const string GivenName = "given_name";
        public const string GivenNames = "given_names";
        public const string FamilyName = "family_name";
        public const string Address = "address";
        public const string Birthdate = "birthdate";
        public const string Email = "email";

        public const string RealmAccess = "realm_access";
        public const string AssuranceLevel = "identity_assurance_level";
        public const string IdentityProvider = "identity_provider";
    }

    public static class FeatureFlags
    {
        public const string VCIssuance = "feature_vc_issuance";
    }

    public static class Roles
    {
        // User Roles
        public const string PrimeEnrollee = "prime_user";
        public const string PrimeSuperAdmin = "prime_super_admin";
        public const string ViewEnrollee = "enrollee_view";
        public const string TriageEnrollee = "enrollee_triage";
        public const string ApproveEnrollee = "enrollee_approve";
        public const string ManageEnrollee = "enrollee_elevated_management";
        public const string ViewSite = "site_view";
        public const string EditSite = "site_edit";
        public const string PrimeMaintenance = "prime_maintenance";

        // Business value role that doesn't represent a permission
        public const string PrimeAdministrant = "prime_administrant";

        // Service Account Roles
        public const string ExternalHpdidAccess = "external_hpdid_access";
        public const string ExternalGpidValidation = "external_gpid_validation";
        public const string PrimeApiServiceAccount = "prime_api_service_account";

        // Roles for External Systems
        public const string PhsaLabtech = "phsa_eforms_labtech";
        public const string PhsaImmunizer = "phsa_eforms_immunizer_covid19";
    }
}
