namespace Prime.ViewModels
{
    public class GisLdapUserViewModel
    {
        public string Authenticated { get; set; }
        public string Unlocked { get; set; }
        public string GisUserRole { get; set; }
        public bool Success { get => GisUserRole == PrimeConfiguration.Current.MohKeycloak.GisUserRole; }
    }
}
