namespace Prime.Models.Api
{
    /// <summary>
    /// Used to pass parameters to api/provisioner-access/gpid-lookup
    /// </summary>
    public class GpidLookupOptions
    {
        public string Gpid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CareSetting { get; set; }
    }
}
