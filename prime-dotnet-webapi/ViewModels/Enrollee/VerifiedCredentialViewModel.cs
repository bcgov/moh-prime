namespace Prime.ViewModels
{
    public class VerifiedCredentialViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public string Base64QRCode { get; set; }
    }
}
