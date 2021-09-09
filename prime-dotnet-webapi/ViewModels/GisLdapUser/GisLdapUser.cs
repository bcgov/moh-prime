namespace Prime.ViewModels
{
    public class GisLdapUser
    {
        public string Unlocked { get; set; }
        public string GisUserRole { get; set; }
        public bool Success { get => GisUserRole == "GISUSER"; }
    }
}
