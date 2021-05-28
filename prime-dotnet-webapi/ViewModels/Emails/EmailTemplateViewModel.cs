using System;
using Prime.Models;

namespace Prime.ViewModels.Emails
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }
        public string Template { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public EmailTemplateType EmailType { get; set; }
    }
}
