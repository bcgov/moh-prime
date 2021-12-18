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
        public bool Notified { get; set; }
        public RemoteUserCertification RemoteUserCertification { get; set; }
    }
}
