using System;
using System.ComponentModel.DataAnnotations;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteBusinessLicenceViewModel
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        public string DeferredLicenceReason { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public Guid? DocumentGuid { get; set; }
    }
}
