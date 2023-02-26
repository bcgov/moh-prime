using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.ViewModels.HealthAuthorities
{
    public class HealthAuthorityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<HealthAuthorityCareTypeViewModel> CareTypes { get; set; }
        /// <summary>
        /// All vendors used at a Health Authority, not filtered by a Care Type
        /// </summary>
        public IEnumerable<HealthAuthorityVendorViewModel> Vendors { get; set; }
        public PrivacyOfficeViewModel PrivacyOffice { get; set; }
        public IEnumerable<TechnicalSupportContactViewModel> TechnicalSupports { get; set; }
        public IEnumerable<HealthAuthorityContactViewModel> PharmanetAdministrators { get; set; }
        public HealthAuthorityOrganizationAgreementDocument HealthAuthorityOrganizationAgreementDocument { get; set; }
    }
}
