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
        public bool PendingTransfer { get; set; }
        public string DoingBusinessAs { get; set; }
        public IEnumerable<CommunitySiteListViewModel> Sites { get; set; }
        public bool Completed { get; set; }
        public bool HasAcceptedAgreement { get; set; }
        public bool HasSubmittedSite { get; set; }
        public bool HasClaim { get; set; }

        public IEnumerable<string> MatchedOn(string textSearch)
        {
            var matchedOn = new List<string>();
            if (textSearch == null)
            {
                return matchedOn;
            }

            textSearch = textSearch.ToLower();

            if (DisplayId.ToString().ToLower().Contains(textSearch))
            {
                matchedOn.Add(nameof(DisplayId));
            }

            if (Name != null && Name.ToLower().Contains(textSearch))
            {
                matchedOn.Add(nameof(Name));
            }

            if (Sites.Any(s => s.DoingBusinessAs != null && s.DoingBusinessAs.ToLower().Contains(textSearch)))
            {
                matchedOn.Add(nameof(Site.DoingBusinessAs));
            }

            if (Sites.Any(s => s.PEC != null && s.PEC.ToLower().Contains(textSearch)))
            {
                matchedOn.Add(nameof(Site.PEC));
            }

            return matchedOn;
        }
    }
}
