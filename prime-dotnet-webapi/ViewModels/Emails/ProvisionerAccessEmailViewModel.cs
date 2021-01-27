namespace Prime.ViewModels.Emails
{
    public class ProvisionerAccessEmailViewModel
    {
        public string EnrolleeFullName { get; set; }
        public string TokenUrl { get; set; }
        public int ExpiresInDays { get; set; }
    }
}
