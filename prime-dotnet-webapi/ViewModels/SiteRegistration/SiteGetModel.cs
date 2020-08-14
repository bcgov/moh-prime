using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SiteGetModel
    {
        public int Id { get; set; }

        public string DoingBusinessAs { get; set; }

        public DateTimeOffset? SubmittedDate { get; set; }

        public string PEC { get; set; }

        public int? OrganizationTypeCode { get; set; }
    }
}
