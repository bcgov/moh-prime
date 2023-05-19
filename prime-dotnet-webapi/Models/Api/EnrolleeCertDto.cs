namespace Prime.Models.Api
{
    public class EnrolleeCertDto
    {
        /// <summary>
        /// Also known as College Prefix
        /// </summary>
        public string PractRefId { get; set; }
        public string CollegeLicenceNumber { get; set; }
        public string PharmaNetId { get; set; }
        /// <summary>
        /// If redacted, other fields should be `null`
        /// </summary>
        public bool Redacted { get; set; }
    }

    public class EnrolleeCertExtDto : EnrolleeCertDto
    {
        public int CollegeCode { get; set; }
    }
}
