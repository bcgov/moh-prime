using Prime.Models;

namespace Prime.ViewModels
{
    public class RemoteAccessSearchViewModel
    {
        public int RemoteUserId { get; set; }

        public string SiteDoingBusinessAs { get; set; }

        public PhysicalAddress SiteAddress { get; set; }
    }
}
