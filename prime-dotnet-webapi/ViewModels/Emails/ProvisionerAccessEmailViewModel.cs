using Prime.Models;

namespace Prime.ViewModels.Emails
{
    public class ProvisionerAccessEmailViewModel
    {
        public string FullName { get; set; }
        public string TokenUrl { get; set; }
        public int ExpiresInDays { get; set; }
    }
}
