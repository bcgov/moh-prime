namespace Prime.Configuration.Api
{
    public static class ProvisionerCareSettingConstants
    {
        public const string PrivateCommunityHealthPractice = "PCHP";
        public const string CommunityPharmacy = "CP";
        public const string NorthernHealthAuthority = "NHA";
        public const string InteriorHealthAuthority = "IHA";
        public const string VancouverCoastalHealthAuthority = "VCHA";
        public const string VancouverIslandHealthAuthority = "VIHA";
        public const string FraserHealthAuthority = "FHA";
        public const string ProvincialHealthServicesAuthority = "PHSA";

        public static readonly string[] ProvisionerCareSettingList = {
            PrivateCommunityHealthPractice,
            CommunityPharmacy,
            NorthernHealthAuthority,
            InteriorHealthAuthority,
            VancouverCoastalHealthAuthority,
            VancouverIslandHealthAuthority,
            FraserHealthAuthority,
            ProvincialHealthServicesAuthority  };
    }
}
