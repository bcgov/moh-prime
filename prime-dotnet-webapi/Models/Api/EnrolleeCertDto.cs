namespace Prime.Models.Api
{
    public class EnrolleeCertDto
    {
        /// <summary>
        /// Identify which college at a high-level, e.g. BC College of Nurses and Midwives (BCCNM)
        /// </summary>
        public int CollegeCode { get; set; }
        /// <summary>
        /// Human-readable translation of CollegeCode
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// Also known as College Prefix
        /// </summary>
        public string CollegeId { get; set; }

        public int LicenseCode { get; set; }
        /// <summary>
        /// Human-readable translation of LicenseCode
        /// </summary>
        public string LicenseName { get; set; }

        public string CollegeLicenseNumber { get; set; }
        public string PharmaNetId { get; set; }
    }
}