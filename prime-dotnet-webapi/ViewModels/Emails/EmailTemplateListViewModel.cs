using System;
using System.Security.Cryptography.X509Certificates;
using Prime.Models;

namespace Prime.ViewModels.Emails
{
    public class EmailTemplateListViewModel
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string Subject { get; set; }
    }
}
