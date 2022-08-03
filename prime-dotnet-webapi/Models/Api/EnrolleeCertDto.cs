namespace Prime.Models.Api
{
    public class EnrolleeCertDto
    {
        /// <summary>
        /// Also known as College Prefix
        /// </summary>
        public string PractRefId { get; set; }

        public string CollegeLicenseNumber { get; set; }
        public string PharmaNetId { get; set; }
    }
}