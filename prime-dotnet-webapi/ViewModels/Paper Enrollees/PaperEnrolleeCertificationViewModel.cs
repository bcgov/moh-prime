using System;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeCertificationsViewModel
    {
        public ICollection<CertificationViewModel> Certifications { get; set; }
    }
}
