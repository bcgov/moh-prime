using System.Collections.Generic;

namespace Prime.ViewModels.Sites
{
    public class RemoteUserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<RemoteUserCertificationViewModel> RemoteUserCertifications { get; set; }
    }
}
