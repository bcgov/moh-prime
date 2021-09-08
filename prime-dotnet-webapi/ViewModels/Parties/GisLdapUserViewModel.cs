namespace Prime.ViewModels
{
    public class GisLdapUserViewModel
    {
        public string RemainingAttempts { get; set; }
        public string GisUserRole { get; set; }
        public bool Success { get => GisUserRole == "GISUSER"; }
    }
}
