namespace Prime.Models.Api
{
    public class GpidLookupOption
    {
        /// <summary>
        /// Used to pass parameters to api/provisioner-access/gpid-lookup
        /// </summary>
        public string Gpid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CareSetting { get; set; }
    }
}
