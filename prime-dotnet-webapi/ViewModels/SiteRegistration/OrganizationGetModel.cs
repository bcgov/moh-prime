using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prime.Models;

namespace Prime.ViewModels
{
    public class OrganizationGetModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SigningAuthorityId { get; set; }

        public Party SigningAuthority { get; set; }

        public ICollection<SiteGetModel> Sites { get; set; }
    }
}
