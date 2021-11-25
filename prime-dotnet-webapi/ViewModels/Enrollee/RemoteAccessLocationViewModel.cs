namespace Prime.ViewModels
{
    public class RemoteAccessLocationViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public string InternetProvider { get; set; }
        public AddressViewModel PhysicalAddress { get; set; }
    }
}
