namespace Prime.ViewModels
{
    public class GisLdapUserViewModel
    {
        public string Unlocked { get; set; }
        public string GisUserRole { get; set; }
        public bool Success { get => GisUserRole == PrimeEnvironment.MohKeycloak.GisUserRole; }
    }
}
