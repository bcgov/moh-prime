using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SigningAuthorityId { get; set; }

        public Party SigningAuthority { get; set; }

        public IEnumerable<SiteViewModel> Sites { get; set; }

        public int DisplayId { get; set; }
    }
}
