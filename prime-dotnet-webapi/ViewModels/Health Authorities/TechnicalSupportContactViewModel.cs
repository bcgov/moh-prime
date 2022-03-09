using System.Collections.Generic;

namespace Prime.ViewModels.HealthAuthorities
{
    public class TechnicalSupportContactViewModel : HealthAuthorityContactViewModel
    {
        /// <summary>
        /// Codes representing the vendors this Technical Support contact works with
        /// </summary>
        public IEnumerable<int> VendorsSupported { get; set; }
    }
}