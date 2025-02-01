using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteRemoteUserUpdateModel
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RemoteUserCertification RemoteUserCertification { get; set; }

        public bool Equals(RemoteUser other)
        {
            if (other == null)
            {
                return false;
            }

            // SiteId of SiteRemoteUserUpdateModel is not alway populated
            // so don't involve in comparison
            return Id == other.Id
                && FirstName == other.FirstName
                && LastName == other.LastName
                && Email == other.Email
                && RemoteUserCertification.CollegeCode == other.RemoteUserCertification.CollegeCode
                && RemoteUserCertification.LicenseCode == other.RemoteUserCertification.LicenseCode
                && RemoteUserCertification.LicenseNumber == other.RemoteUserCertification.LicenseNumber
                && RemoteUserCertification.PractitionerId == other.RemoteUserCertification.PractitionerId;
        }
    }
}
