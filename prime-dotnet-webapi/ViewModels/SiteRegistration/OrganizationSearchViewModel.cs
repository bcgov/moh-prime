using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class OrganizationSearchViewModel
    {
        public IEnumerable<string> MatchedOn { get; set; }
        public OrganizationListViewModel Organization { get; set; }
    }
}
