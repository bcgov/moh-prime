using System;
using System.Linq;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class OrganizationListViewModel
    {
        public int Id { get; set; }
        public int DisplayId { get; set; }
        public string RegistrationId { get; set; }
        public string Name { get; set; }
        public int SigningAuthorityId { get; set; }
        public Party SigningAuthority { get; set; }
        public string DoingBusinessAs { get; set; }
        public IEnumerable<SiteListViewModel> Sites { get; set; }
        public bool Completed { get; set; }
        public bool HasAcceptedAgreement { get; set; }
        public bool HasSubmittedSite { get; set; }

        public IEnumerable<string> MatchedOn(string textSearch)
        {
            textSearch = textSearch.ToLower();
            var matchedOn = new List<string>();

            if (DisplayId.ToString().ToLower().Contains(textSearch))
            {
                matchedOn.Add("Reference Id");
            }

            if (Name.ToLower().Contains(textSearch))
            {
                matchedOn.Add("Organization Name");
            }

            if (Sites.Any(s => s.DoingBusinessAs.ToLower().Contains(textSearch)))
            {
                matchedOn.Add("Site Name");
            }

            if (Sites.Any(s => s.PEC.ToLower().Contains(textSearch)))
            {
                matchedOn.Add("Site ID");
            }

            return matchedOn;
        }
    }
}
