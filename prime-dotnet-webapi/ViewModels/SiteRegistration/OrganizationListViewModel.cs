using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class OrganizationListViewModel
    {
        public int Id { get; set; }

        public int DisplayId { get; set; }

        public string Name { get; set; }

        public int SigningAuthorityId { get; set; }

        public Party SigningAuthority { get; set; }

        public IEnumerable<SiteListViewModel> Sites { get; set; }

        public int SignedAgreementDocumentCount { get; set; }

        public bool Completed { get; set; }

        public DateTimeOffset? AcceptedAgreementDate { get; set; }
    }
}
